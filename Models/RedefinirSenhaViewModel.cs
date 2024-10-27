using System.ComponentModel.DataAnnotations;

namespace NewRepository.Models
{
    public class RedefinirSenhaViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }
    }

}
