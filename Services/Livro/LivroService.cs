using Microsoft.EntityFrameworkCore;
using NewRepository.Dto;
using NewRepository.Models;
using System.Linq.Expressions;

namespace NewRepository.Services.Livro
{
    // Implementação do ILivroInterface
    public class LivroService : ILivroInterface
    {
        private readonly Contexto _contexto;
        private readonly string _sistema;

        public LivroService(Contexto contexto, IWebHostEnvironment sistema)
        {
            _contexto = contexto;
            _sistema = sistema.WebRootPath;
        }

        // Método para gerar caminho único para a imagem do livro
        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoImage = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";
            var caminhoParaSalvarImagens = _sistema + "\\imagem\\";

            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            using (var stream = File.Create(caminhoParaSalvarImagens + nomeCaminhoImage))
            {
                foto.CopyToAsync(stream).Wait();
            }

            return nomeCaminhoImage;
        }

        // Método para criar um livro
        public async Task<LivroModel> CriarLivro(LivroCriacaoDto livroCriacaoDto, IFormFile foto, int usuarioId)
        {
            try
            {
                var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                // Busca o usuário (instituição) pelo ID fornecido
                var usuario = await _contexto.Instituicoes.FirstOrDefaultAsync(u => u.Id == usuarioId);

                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                var livro = new LivroModel
                {
                    Capa = nomeCaminhoImagem, // Salvando o caminho da capa
                    Isbn = livroCriacaoDto.Isbn,
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = livroCriacaoDto.Autor,
                    AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                    NomeEditatora = livroCriacaoDto.NomeEditatora,
                    Genero = livroCriacaoDto.Genero,
                    DataAdd = livroCriacaoDto.DataAdd,
                    UsuarioId = usuarioId,
                    Usuario = usuario // Atribuindo o objeto Usuario
                };

                _contexto.Add(livro);
                await _contexto.SaveChangesAsync();

                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // Método para obter todos os livros
        public async Task<List<LivroModel>> GetLivros()
        {
            try
            {
                return await _contexto.Livros.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para obter um livro por ID
        public async Task<LivroModel> GetLivroPorId(int id)
        {
            try
            {
                return await _contexto.Livros.FirstOrDefaultAsync(livro => livro.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para editar um livro
        public async Task<LivroModel> EditarLivro(LivroModel livro, IFormFile? foto)
        {
            try
            {
                var livroBanco = await _contexto.Livros.AsNoTracking().FirstOrDefaultAsync(livroBD => livroBD.Id == livro.Id);
                var nomeCaminhoImagem = "";

                if (foto != null)
                {
                    string caminhoCapaExistente = _sistema + "\\imagem\\" + livroBanco.Capa;

                    if (File.Exists(caminhoCapaExistente))
                    {
                        File.Delete(caminhoCapaExistente);
                    }

                    nomeCaminhoImagem = GeraCaminhoArquivo(foto);
                }

                livroBanco.Isbn = livro.Isbn;
                livroBanco.Titulo = livro.Titulo;
                livroBanco.Autor = livro.Autor;
                livroBanco.AnoPublicacao = livro.AnoPublicacao;
                livroBanco.NomeEditatora = livro.NomeEditatora;
                livroBanco.Genero = livro.Genero;
                livroBanco.DataAdd = livro.DataAdd;

                if (!string.IsNullOrEmpty(nomeCaminhoImagem))
                {
                    livroBanco.Capa = nomeCaminhoImagem;
                }

                _contexto.Update(livroBanco);
                await _contexto.SaveChangesAsync();

                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para remover um livro
        public async Task<LivroModel> RemoverLivro(int id)
        {
            try
            {
                var livro = await _contexto.Livros.FirstOrDefaultAsync(livroBanco => livroBanco.Id == id);
                _contexto.Remove(livro);
                await _contexto.SaveChangesAsync();
                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para buscar livros com filtro
        public async Task<List<LivroModel>> GetLivrosFiltro(string? pesquisar)
        {
            try
            {
                return await _contexto.Livros
                    .Where(livroBanco => livroBanco.Titulo.Contains(pesquisar))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LivroModel>> GetLivrosPorUsuario(int usuarioId)
        {
            try
            {
                return await _contexto.Livros
                    .Where(livro => livro.UsuarioId == usuarioId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LivroModel?> GetLivroPorIsbnEUsuario(string isbn, int usuarioId)
        {
            return await _contexto.Livros
                .FirstOrDefaultAsync(l => l.Isbn == isbn && l.UsuarioId == usuarioId); // Verifica ISBN e ID do usuário
        }
    }
}
