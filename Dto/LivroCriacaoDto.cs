namespace NewRepository.Dto
{
    public class LivroCriacaoDto
    {
        public string Capa { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;      // Código ISBN do livro (International Standard Book Number)
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public DateTime AnoPublicacao { get; set; }
        public string NomeEditatora { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;      // Gênero do livro
        public DateTime DataAdd { get; set; } // Data em que o livro foi adicionado à biblioteca
    }
}
