namespace NewRepository.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

    }
}
