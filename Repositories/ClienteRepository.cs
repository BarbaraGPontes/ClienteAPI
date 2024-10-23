using ClienteAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClienteAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private const string filePath = "Data/clientes.json";

        public IEnumerable<Cliente> ListarClientes(string nome, string email, string cpf)
        {
            var clientes = LerClientesDoArquivo();
            if (!string.IsNullOrEmpty(nome))
            {
                clientes = clientes.Where(c => c.Nome.Contains(nome)).ToList();
            }
            if (!string.IsNullOrEmpty(email))
            {
                clientes = clientes.Where(c => c.Email.Contains(email)).ToList();
            }
            if (!string.IsNullOrEmpty(cpf))
            {
                clientes = clientes.Where(c => c.CPF.Contains(cpf)).ToList();
            }
            return clientes;
        }

        public void AdicionarCliente(Cliente cliente)
        {
            var clientes = LerClientesDoArquivo().ToList();
            cliente.Id = clientes.Count > 0 ? clientes.Max(c => c.Id) + 1 : 1;
            clientes.Add(cliente);
            EscreverClientesNoArquivo(clientes);
        }

        public void AtualizarCliente(Cliente cliente)
        {
            var clientes = LerClientesDoArquivo().ToList();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == cliente.Id);
            if (clienteExistente != null)
            {
                clienteExistente.Nome = cliente.Nome;
                clienteExistente.Email = cliente.Email;
                clienteExistente.CPF = cliente.CPF;
                clienteExistente.RG = cliente.RG;

                clienteExistente.Contatos = cliente.Contatos;
                clienteExistente.Enderecos = cliente.Enderecos;

                EscreverClientesNoArquivo(clientes);
            }
        }

        public void RemoverCliente(int id)
        {
            var clientes = LerClientesDoArquivo().ToList();
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                clientes.Remove(cliente);
                EscreverClientesNoArquivo(clientes);
            }
        }

        public void AdicionarOuAtualizarContato(int clienteId, Contatos contato)
        {
            var clientes = LerClientesDoArquivo().ToList();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == clienteId);
            if (clienteExistente != null)
            {
                var contatoExistente = clienteExistente.Contatos.FirstOrDefault(c => c.Id == contato.Id);
                if (contatoExistente != null)
                {
                    contatoExistente.Tipo = contato.Tipo;
                    contatoExistente.DDD = contato.DDD;
                    contatoExistente.Telefone = contato.Telefone;
                }
                else
                {
                    clienteExistente.Contatos.Add(contato);
                }
                EscreverClientesNoArquivo(clientes);
            }
        }

        public void RemoverContato(int clienteId, int contatoId)
        {
            var clientes = LerClientesDoArquivo().ToList();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == clienteId);
            if (clienteExistente != null)
            {
                var contato = clienteExistente.Contatos.FirstOrDefault(c => c.Id == contatoId);
                if (contato != null)
                {
                    clienteExistente.Contatos.Remove(contato);
                    EscreverClientesNoArquivo(clientes);
                }
            }
        }

        public void AdicionarOuAtualizarEndereco(int clienteId, Enderecos endereco)
        {
            var clientes = LerClientesDoArquivo().ToList();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == clienteId);
            if (clienteExistente != null)
            {
                var enderecoExistente = clienteExistente.Enderecos.FirstOrDefault(e => e.Id == endereco.Id);
                if (enderecoExistente != null)
                {
                    enderecoExistente.Tipo = endereco.Tipo;
                    enderecoExistente.CEP = endereco.CEP;
                    enderecoExistente.Logradouro = endereco.Logradouro;
                    enderecoExistente.Numero = endereco.Numero;
                    enderecoExistente.Bairro = endereco.Bairro;
                    enderecoExistente.Complemento = endereco.Complemento;
                    enderecoExistente.Cidade = endereco.Cidade;
                    enderecoExistente.Estado = endereco.Estado;
                    enderecoExistente.Referencia = endereco.Referencia;
                }
                else
                {
                    clienteExistente.Enderecos.Add(endereco);
                }
                EscreverClientesNoArquivo(clientes);
            }
        }

        public void RemoverEndereco(int clienteId, int enderecoId)
        {
            var clientes = LerClientesDoArquivo().ToList();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == clienteId);
            if (clienteExistente != null)
            {
                var endereco = clienteExistente.Enderecos.FirstOrDefault(e => e.Id == enderecoId);
                if (endereco != null)
                {
                    clienteExistente.Enderecos.Remove(endereco);
                    EscreverClientesNoArquivo(clientes);
                }
            }
        }

        private List<Cliente> LerClientesDoArquivo()
        {
            if (!File.Exists(filePath))
            {
                return new List<Cliente>();
            }
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Cliente>>(json) ?? new List<Cliente>();
        }

        private void EscreverClientesNoArquivo(List<Cliente> clientes)
        {
            var json = JsonConvert.SerializeObject(clientes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
