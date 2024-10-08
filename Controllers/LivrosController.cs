using Microsoft.AspNetCore.Mvc;
using NewRepository.Dto;
using NewRepository.Filtros;
using NewRepository.Models;
using NewRepository.Services.Livro;
using NewRepository.Services.SessaoService; // Para buscar o usuário logado
using System.Collections.Generic; // Certifique-se de incluir a diretiva correta
using System.Threading.Tasks;

namespace NewRepository.Controllers
{
    [UsuarioLogado]
    public class LivrosController : Controller
    {
        private readonly ILivroInterface _livroInterface;
        private readonly ISessaoInterface _sessaoService;

        public LivrosController(ILivroInterface livroInterface, ISessaoInterface sessaoService)
        {
            _livroInterface = livroInterface;
            _sessaoService = sessaoService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioLogado = _sessaoService.BuscarSessao();

            List<LivroModel> livros;

            if (usuarioLogado != null)
            {
                // Retorna apenas os livros cadastrados pelo usuário logado
                livros = await _livroInterface.GetLivrosPorUsuario(usuarioLogado.Id);
            }
            else
            {
                // Retorna todos os livros
                livros = await _livroInterface.GetLivros();
            }

            return View(livros);
        }

        // Novo método para listar os livros cadastrados pelo usuário logado
        public async Task<IActionResult> MeusLivros()
        {
            var usuarioLogado = _sessaoService.BuscarSessao(); // Buscar a biblioteca logada

            if (usuarioLogado == null)
            {
                return Unauthorized(); // Caso a sessão esteja expirada ou inválida
            }

            var livros = await _livroInterface.GetLivrosPorUsuario(usuarioLogado.Id); // Método que busca os livros do usuário logado
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
                var usuarioLogado = _sessaoService.BuscarSessao(); // Buscar a biblioteca logada

                if (usuarioLogado == null)
                {
                    return Unauthorized(); // Caso a sessão esteja expirada ou inválida
                }

                var livro = await _livroInterface.CriarLivro(livroCriacaoDto, foto, usuarioLogado.Id); // Passa o ID da biblioteca logada
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
                return RedirectToAction("Index", "Livros");
            }
            else
            {
                return View(livroModel);
            }
        }
    }
}
