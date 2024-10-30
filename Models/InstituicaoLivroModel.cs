namespace NewRepository.Models
{
    public class InstituicaoLivroModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public int Quantidade { get; set; }

        // Relacionamentos
        public UsuarioModel Usuario { get; set; }
        public LivroModel Livro { get; set; } // Para acessar detalhes do livro pelo ISBN
    }
}