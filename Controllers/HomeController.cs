using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NewRepository.Dto;
using NewRepository.Filtros;
using NewRepository.Models;
using NewRepository.Services.Livro;
using NewRepository.Services.SessaoService;
using NewRepository.Services.UsuarioService;

namespace NewRepository.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILivroInterface _livroInterface;
        private readonly IUsuarioInterface _usuarioInterface;
        private readonly ISessaoInterface _sessaoInterface;

        public HomeController(ILivroInterface livroInterface, IUsuarioInterface usuarioInterface, ISessaoInterface sessaoInterface)
        {
            _livroInterface = livroInterface;
            _usuarioInterface = usuarioInterface;
            _sessaoInterface = sessaoInterface;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessaoInterface.RemoverSessao();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(string? pesquisar)
        {
            var usuarioLogado = _sessaoInterface.BuscarSessao(); // Verifica se o usu�rio est� logado

            // Sempre retorna todos os livros, independentemente do status de login
            var livros = pesquisar == null
                ? await _livroInterface.GetLivros() // Busca todos os livros
                : await _livroInterface.GetLivrosFiltro(pesquisar); // Busca com filtro

            return View(livros);
        }


        public IActionResult Home()
        {
            return RedirectToAction("Index", "Livros");
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Login(loginDto);

                if (usuario.Id == 0)
                {
                    TempData["MensagemErro"] = "Credenciais inv�lidas!";
                    return View(loginDto);
                }
                else
                {
                    TempData["MensagemSucesso"] = "Usu�rio logado com sucesso!";
                    _sessaoInterface.CriarSessao(usuario);
                    return RedirectToAction("Index", "Livros");
                }
            }
            else
            {
                return View(loginDto);
            }
        }
    }
}
