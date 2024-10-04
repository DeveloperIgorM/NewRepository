using System.ComponentModel.DataAnnotations;

namespace NewRepository.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        
        public string Email { get; set; }

        [Required]
        public string RazaoSocial { get; set; } = string.Empty;

        public string NomeFantasia { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"\d{14}", ErrorMessage = "CNPJ inválido.")]
        public string Cnpj { get; set; } = string.Empty;

        [Phone]
        public string Telefone { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        [Required]
        public byte[] SenhaHash { get; set; }

        [Required]
        public byte[] SenhaSalt { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Inicializa a coleção no construtor
        public ICollection<LivroModel> Livros { get; set; } = new List<LivroModel>();
    }
}
