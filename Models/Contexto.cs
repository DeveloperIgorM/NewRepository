using Microsoft.EntityFrameworkCore;

namespace NewRepository.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Biblioteca> Bibliotecas { get; set; }
        public DbSet<LivroModel> Livros { get; set; }

        public DbSet<UsuarioModel> Instituicoes { get; set; }
        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
