using Microsoft.EntityFrameworkCore;

namespace NewRepository.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Biblioteca> Bibliotecas { get; set; }
        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<UsuarioModel> Instituicoes { get; set; } // Representa as Instituições (ou Bibliotecas)
        public DbSet<InstituicaoLivroModel> InstituicaoLivros { get; set; } // Tabela intermediária para quantidades de livros por instituição

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de relacionamento entre LivroModel e UsuarioModel (Instituição)
            modelBuilder.Entity<LivroModel>()
                .HasOne(l => l.Usuario) // Propriedade de navegação em LivroModel
                .WithMany(u => u.Livros) // Propriedade de coleção em UsuarioModel
                .HasForeignKey(l => l.UsuarioId);

            // Configuração de relacionamento entre InstituicaoLivroModel e UsuarioModel (Instituição)
            modelBuilder.Entity<InstituicaoLivroModel>()
                .HasOne(il => il.Usuario) // Propriedade de navegação em InstituicaoLivroModel
                .WithMany(i => i.InstituicaoLivros) // Propriedade de coleção em UsuarioModel
                .HasForeignKey(il => il.UsuarioId);

            // Configuração de relacionamento entre InstituicaoLivroModel e LivroModel usando ISBN
            modelBuilder.Entity<InstituicaoLivroModel>()
                .HasOne(il => il.Livro) // Propriedade de navegação em InstituicaoLivroModel
                .WithMany(l => l.InstituicaoLivros) // Propriedade de coleção em LivroModel
                .HasForeignKey(il => il.Isbn) // Associa InstituicaoLivro ao LivroModel pelo ISBN
                .HasPrincipalKey(l => l.Isbn); // Configura ISBN como chave principal no relacionamento

            base.OnModelCreating(modelBuilder);
        }
    }
}
