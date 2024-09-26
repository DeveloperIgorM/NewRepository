using Microsoft.AspNetCore.Mvc;
using NewRepository.Dto;
using NewRepository.Services.Livro;
using System.Diagnostics;


namespace NewRepository.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ILivroInterface _livroInterface;

        public LivrosController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar() { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var livro = await _livroInterface.CriarLivro(livroCriacaoDto, foto);
                return RedirectToAction("Index", "Livros");
            }
            else
            {
                return View(livroCriacaoDto);
            }
        }
    }     
}
