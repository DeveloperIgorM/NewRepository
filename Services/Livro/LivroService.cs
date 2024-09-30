using Microsoft.EntityFrameworkCore;
using NewRepository.Dto;
using NewRepository.Models;
using System.Linq.Expressions;

namespace NewRepository.Services.Livro
{

    //Todos os métodos que estiverem na interface, vão implementar o service
    public class LivroService : ILivroInterface
    {


        private readonly Contexto _contexto;
        private readonly string _sistema;

        //Método construt
        public LivroService(Contexto contexto, IWebHostEnvironment sistema)
        {
            _contexto = contexto;
            _sistema = sistema.WebRootPath;
        }

        //Método para armazenar as imagens dos livros

        //Armazenar código de nome de imagem único  
        public string GeraCaminhoArquivo(IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoImage = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";

            var caminhoParaSalvarImagens = _sistema + "\\imagem\\";

            //Verifica se a pasta Imagem existe, se não existir, crie!
            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }
            using (var stream = File.Create(caminhoParaSalvarImagens + nomeCaminhoImage))
            {
                foto.CopyToAsync(stream).Wait(); //criando uma foto dentro do caminho de imagem
            }

            return nomeCaminhoImage;

        }

        public async Task<LivroModel> CriarLivro(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            try
            {
                var nomeCaminhoImagem = GeraCaminhoArquivo(foto);

                var livro = new LivroModel
                {
                    Capa = livroCriacaoDto.Capa,
                    Isbn = livroCriacaoDto.Isbn,
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = livroCriacaoDto.Autor,
                    AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                    NomeEditatora = livroCriacaoDto.NomeEditatora,
                    Genero = livroCriacaoDto.Genero,
                    DataAdd = livroCriacaoDto.DataAdd
                };

                _contexto.Add(livro);
                await _contexto.SaveChangesAsync();

                return livro;
            }
            catch (Exception ex)
            {
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<LivroModel>> GetLivros()
        {
           try
            {
                return await _contexto.Livros.ToListAsync();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LivroModel> GetLivroPorId(int id)
        {
            try
            {
                return await _contexto.Livros.FirstOrDefaultAsync(livro => livro.Id == id);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
                
                if (nomeCaminhoImagem != "")
                {
                    livroBanco.Capa = nomeCaminhoImagem;
                }
                else
                {
                    livroBanco.Capa = livro.Capa;
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

        public async Task<LivroModel> RemoverLivro(int id)
        {
            try
            {
                //banco de dados, tabela no banco de dados e parametros
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

        public async Task<List<LivroModel>> GetLivrosFiltro(string? pesquisar)
        {
            try
            {
               var livros = await _contexto.Livros.Where(livroBanco => livroBanco.Titulo.Contains(pesquisar)).ToListAsync();
                return livros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}