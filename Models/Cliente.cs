using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClienteAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")] 
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CPF deve ter no máximo 14 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O RG é obrigatório.")]
        [StringLength(12, ErrorMessage = "O RG deve ter no máximo 12 caracteres.")]
        public string RG { get; set; }

        public List<Contatos> Contatos { get; set; } = new List<Contatos>();
        public List<Enderecos> Enderecos { get; set; } = new List<Enderecos>();
    }
}
