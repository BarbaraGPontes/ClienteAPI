using ClienteAPI.Models;

namespace ClienteAPI.Repositories
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> ListarClientes(string nome, string email, string cpf);
        void AdicionarCliente(Cliente cliente);
        void AtualizarCliente(Cliente cliente);
        void RemoverCliente(int id);

        void AdicionarOuAtualizarContato(int clienteId, Contatos contato);
        void RemoverContato(int clienteId, int contatoId);
        void AdicionarOuAtualizarEndereco(int clienteId, Enderecos endereco);
        void RemoverEndereco(int clienteId, int enderecoId);
    }
}
