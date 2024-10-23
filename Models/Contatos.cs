using System.ComponentModel.DataAnnotations;

namespace ClienteAPI.Models
{
    public class Contatos
    {
        public int Id { get; set; }

        public string? Tipo { get; set; } // Residencial, Comercial, Celular

        public int? DDD { get; set; }

        [RegularExpression(@"^\d{4,5}-\d{4}$", ErrorMessage = "O telefone deve estar no formato XXXXX-XXXX ou XXXX-XXXX.")]
        public string? Telefone { get; set; } //como daria p ser decimal?
    }
}
