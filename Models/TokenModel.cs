namespace NewRepository.Models
{
    public class TokenModel
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
