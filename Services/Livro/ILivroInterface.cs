using NewRepository.Dto;
using NewRepository.Models;

namespace NewRepository.Services.Livro
{
    public interface ILivroInterface
    {
        Task<LivroModel> CriarLivro(LivroCriacaoDto livroCriacaoDto, IFormFile foto, int usuarioId); // Adicione o ID do usuário
        Task<List<LivroModel>> GetLivros();
        Task<LivroModel> GetLivroPorId(int id);
        Task<LivroModel> EditarLivro(LivroModel livro, IFormFile? foto);
        Task<LivroModel> RemoverLivro(int id);
        Task<List<LivroModel>> GetLivrosFiltro(string? pesquisar);
        Task<List<LivroModel>> GetLivrosPorUsuario(int usuarioId);
        Task<LivroModel?> GetLivroPorIsbnEUsuario(string isbn, int usuarioId); // Apenas assinatura do método

    }
}
