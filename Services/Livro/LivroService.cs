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
    }
}