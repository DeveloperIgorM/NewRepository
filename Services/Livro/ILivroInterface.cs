using NewRepository.Dto;
using NewRepository.Models;


namespace NewRepository.Services.Livro
{



    public interface ILivroInterface
    {
        Task<LivroModel> CriarLivro(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
    }
}