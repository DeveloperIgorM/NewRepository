using Microsoft.EntityFrameworkCore;

namespace NewRepository.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Biblioteca> Bibliotecas { get; set; }
        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<UsuarioModel> Instituicoes { get; set; } // Representa a Biblioteca


        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LivroModel>()
                .HasOne(l => l.Usuario)
                .WithMany(u => u.Livros)
                .HasForeignKey(l => l.UsuarioId);
        }
    }
}
