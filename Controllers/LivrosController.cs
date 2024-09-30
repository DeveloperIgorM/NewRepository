using Microsoft.AspNetCore.Mvc;
using NewRepository.Dto;
using NewRepository.Models;
using NewRepository.Services.Livro;
using SQLitePCL;
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
        public async Task<IActionResult> Index()
        {
            var livros = await _livroInterface.GetLivros();
            return View(livros);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var livro = await _livroInterface.GetLivroPorId(id);
            return View(livro);
        }

        public async Task<IActionResult> Editar(int id)
        {
             var livro = await _livroInterface.GetLivroPorId(id);
            return View(livro);
        }

        public async Task<IActionResult> Remover(int id)
        {
            var livro = await _livroInterface.RemoverLivro(id);
            return RedirectToAction("Index", "Livros");
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

        [HttpPost]

        public async Task<IActionResult> Editar(LivroModel livroModel, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
                var livro = await _livroInterface.EditarLivro(livroModel, foto);
                return RedirectToAction("Index", "Livros"); //Livros = LivrosController
            }
            else
            {
                return View(livroModel);
            }

            
        }

    }     
}
