using System.ComponentModel.DataAnnotations;

namespace NewRepository.Dto
{
    public class UsuarioCriacaoDto
    {
        [Required(ErrorMessage = "Insira um usuário!")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Insira um Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira uma senha!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Insira um usuário!"),
            Compare("Senha", ErrorMessage = "Senhas estão diferentes!")]
        public string ConfirmaSenha { get; set; }
    }
}
