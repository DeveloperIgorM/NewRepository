using Microsoft.AspNetCore.Mvc;
using NewRepository.Dto;
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


        [HttpPost]

        public async Task<ActionResult<UsuarioModel>> Login(LoginDto loginDto)
        {

            if (ModelState.IsValid)
            {
                var usuario = await _usuarioInterface.Login(loginDto);

                if (usuario.Id == 0)
                {
                    TempData["MensagemErro"] = "Credenciais inválidas!";
                    return View(loginDto);
                }
                else
                {
                    TempData["MensagemSucesso"] = "Usuário logado com sucesso!";


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