namespace NewRepository.Models
{
    public class InstituicaoLivroModel
    {
        public int Id { get; set; }  // Identificador único para o relacionamento

        public int UsuarioId { get; set; }  // Alterado para InstituicaoId (relacionando a Instituicao)
        public int LivroId { get; set; }  // LivroId está correto (relacionando a Livro)
        public int Quantidade { get; set; }
      
        // Relacionamentos
        public UsuarioModel Usuario { get; set; }  // Relacionamento com a InstituicaoModel
        public LivroModel Livro { get; set; }  // Relacionamento com a LivroModel
    }
}
