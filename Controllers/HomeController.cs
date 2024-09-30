using Microsoft.AspNetCore.Mvc;
using NewRepository.Services.Livro;

namespace NewRepository.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILivroInterface _livroInterface;
        public HomeController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }
        public async Task<IActionResult> Index(string? pesquisar)
        {
            if (pesquisar == null)
            {
                var livros = await _livroInterface.GetLivros();
                return View(livros);
            }
            else
            {
                var livros = await _livroInterface.GetLivrosFiltro(pesquisar);
                return View(livros);
            }
        }
    }
}