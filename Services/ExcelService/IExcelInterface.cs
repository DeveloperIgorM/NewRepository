using NewRepository.Filtros;
using NewRepository.Models;

namespace NewRepository.Services
{
    public interface IExcelInterface
    {
        MemoryStream LerStream(IFormFile formFile);
        List<LivroModel> LerXls(MemoryStream stream, int usuarioLogadoId);
        void SalvarDados(List<LivroModel> livros, int usuarioId);
    }
}