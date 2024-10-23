using ClienteAPI.Models;
using ClienteAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Infrastructure;

namespace ClienteAPI.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Listar(string nome, string email, string cpf)
        {
            var clientes = _clienteRepository.ListarClientes(nome, email, cpf);
            ViewBag.Nome = nome;
            ViewBag.Email = email;
            ViewBag.CPF = cpf;

            return View(clientes);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Cliente cliente)
        {
            if (cliente.CPF != null && cliente.RG != null)
            {
                _clienteRepository.AdicionarCliente(cliente);
                return RedirectToAction(nameof(Listar));
            }

            foreach (var state in ModelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(cliente);
        }

        public IActionResult Atualizar(int id)
        {
            var cliente = _clienteRepository.ListarClientes(null, null, null).FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Atualizar(int id, Cliente clienteAtualizado)
        {
            if (id != clienteAtualizado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clienteRepository.AtualizarCliente(clienteAtualizado);
                    return RedirectToAction(nameof(Listar));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao atualizar os dados: " + ex.Message);
                }
            }

            return View(clienteAtualizado);
        }

        public IActionResult Remover(int id)
        {
            var cliente = _clienteRepository.ListarClientes(null, null, null).FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            _clienteRepository.RemoverCliente(id);
            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult AdicionarOuAtualizarContato(int clienteId, Contatos contato)
        {
            _clienteRepository.AdicionarOuAtualizarContato(clienteId, contato);
            return RedirectToAction(nameof(Atualizar), new { id = clienteId });
        }

        [HttpPost]
        public IActionResult RemoverContato(int clienteId, int contatoId)
        {
            _clienteRepository.RemoverContato(clienteId, contatoId);
            return RedirectToAction(nameof(Atualizar), new { id = clienteId });
        }

        [HttpPost]
        public IActionResult AdicionarOuAtualizarEndereco(int clienteId, Enderecos endereco)
        {
            _clienteRepository.AdicionarOuAtualizarEndereco(clienteId, endereco);
            return RedirectToAction(nameof(Atualizar), new { id = clienteId });
        }

        [HttpPost]
        public IActionResult RemoverEndereco(int clienteId, int enderecoId)
        {
            _clienteRepository.RemoverEndereco(clienteId, enderecoId);
            return RedirectToAction(nameof(Atualizar), new { id = clienteId });
        }
    }
}
