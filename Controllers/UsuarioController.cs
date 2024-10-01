using Microsoft.AspNetCore.Mvc;
using NewRepository.Dto;
using NewRepository.Models;
using NewRepository.Services.UsuarioService;

namespace NewRepository.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            if (ModelState.IsValid)
            {
                //cadastrar usuário no banco e atribuir a variável "usuario"
                var usuario = await _usuarioInterface.Cadastrar(usuarioCriacaoDto);

                if(usuario != null)
                {
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index", "Livros");
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro no momento do cadastro!";
                    return View(usuarioCriacaoDto);
                }

            }
            else
            {
                return View(usuarioCriacaoDto);
            }
        }

    }
}