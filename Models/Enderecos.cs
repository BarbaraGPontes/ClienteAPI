using System.ComponentModel.DataAnnotations;

namespace ClienteAPI.Models
{
    public class Enderecos
    {
        public int Id { get; set; }

        public string? Tipo { get; set; } // Preferencial, Entrega, Cobrança

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP inválido. Formato: 12345-678")]
        public string CEP { get; set; }

        public string? Logradouro { get; set; }

        public int? Numero { get; set; }

        public string? Bairro { get; set; }

        public string? Complemento { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public string Estado { get; set; }

        public string? Referencia { get; set; }
    }
}
