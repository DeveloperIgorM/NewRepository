using System.ComponentModel.DataAnnotations;

namespace NewRepository.Dto
{
    public class UsuarioCriacaoDto
    {      

        [Required(ErrorMessage = "Insira um Email!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Insira a razão social!")]
        public string RazaoSocial { get; set; } = string.Empty;


        [Required(ErrorMessage = "Insira o nome fantasia!")]
        public string NomeFantasia { get; set; } = string.Empty;


        [Required(ErrorMessage = "Insira o CNPJ!")]
        [RegularExpression(@"\d{14}", ErrorMessage = "CNPJ deve conter 14 dígitos!")]
        public string Cnpj { get; set; } = string.Empty;


        [Required(ErrorMessage = "Insira o telefone!")]
        public string Telefone { get; set; } = string.Empty;


        [Required(ErrorMessage = "Insira o endereço!")]
        public string Endereco { get; set; } = string.Empty;


        [Required(ErrorMessage = "Insira uma senha!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme sua senha!")]
        [Compare("Senha", ErrorMessage = "Senhas estão diferentes!")]
        public string ConfirmaSenha { get; set; }
    }
}
