namespace NewRepository.Models
{
    public class LivroModel

    {
        public int Id { get; set; }
        public string Capa { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;      // Código ISBN do livro (International Standard Book Number)
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string AnoPublicacao { get; set; }
        public string NomeEditatora { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;      // Gênero do livro
        public DateTime DataAdd { get; set; } // Data em que o livro foi adicionado à biblioteca
        public int QtdLivro { get; set; }
        public string FonteCadastro { get; set; }

        public int UsuarioId { get; set; } // Referência à biblioteca
        public UsuarioModel Usuario { get; set; } // Navegação para o usuário
    }
}
