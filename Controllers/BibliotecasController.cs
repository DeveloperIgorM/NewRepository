using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewRepository.Models;

namespace NewRepository.Controllers
{
    public class BibliotecasController : Controller
    {

        private readonly Contexto _contexto;

        //Injeção de depêndencias
        public BibliotecasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Bibliotecas.ToListAsync());
        }

    
        [HttpGet]
        public IActionResult NovaBiblioteca()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovaBiblioteca(Biblioteca biblioteca)
        {
            await _contexto.Bibliotecas.AddAsync(biblioteca);
            await _contexto.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}