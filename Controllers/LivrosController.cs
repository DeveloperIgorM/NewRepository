using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewRepository.Dto;
using NewRepository.Filtros;
using NewRepository.Models;
using NewRepository.Services.Livro;
using NewRepository.Services.SessaoService;
using System.Data; // Para buscar o usuário logado


namespace NewRepository.Controllers
{
    
    public class LivrosController : Controller
    {
        private readonly Contexto _context;
        private readonly ILivroInterface _livroInterface;
        private readonly ISessaoInterface _sessaoService;
        
       

        public LivrosController(ILivroInterface livroInterface, ISessaoInterface sessaoService, Contexto context)
        {
            _livroInterface = livroInterface;
            _sessaoService = sessaoService;
            _context = context;
        }
        [UsuarioLogado]
        public async Task<IActionResult> Index()
        {
            var usuarioLogado = _sessaoService.BuscarSessao();
            List<LivroModel> livros;

            if (usuarioLogado != null)
            {
                ViewBag.NomeFantasia = usuarioLogado.NomeFantasia; // Passa o NomeFantasia para a ViewBag
            }

            if (usuarioLogado != null)
            {
                // Retorna apenas os livros cadastrados pelo usuário logado
                livros = await _livroInterface.GetLivrosPorUsuario(usuarioLogado.Id);
                ViewBag.UsuarioLogado = true; // Define que o usuário está logado
            }
            else
            {
                // Retorna todos os livros
                livros = await _livroInterface.GetLivros();
                ViewBag.UsuarioLogado = false; // Define que o usuário não está logado
            }

            return View(livros);
        }


        public IActionResult Cadastrar()
        {
            var usuarioLogado = _sessaoService.BuscarSessao();
            if (usuarioLogado == null)
            {
                ViewBag.UsuarioLogado = false; // Define que o usuário não está logado
                return Unauthorized(); // Caso a sessão esteja expirada ou inválida
            }

            ViewBag.UsuarioLogado = true; // Define que o usuário está logado
            return View();
        }


        // Permite que qualquer um acesse o método, mesmo sem estar logado
        public async Task<IActionResult> Detalhes(int id)
        {
            var livro = await _livroInterface.GetLivroPorId(id);

            if (livro == null)
            {
                return NotFound();
            }

            // Verificar se o usuário está logado
            var usuarioLogado = _sessaoService.BuscarSessao(); // Verifica se o usuário está logado
            ViewBag.UsuarioLogado = usuarioLogado != null;

            if (usuarioLogado != null)
            {
                ViewBag.NomeFantasia = usuarioLogado.NomeFantasia; // Enviar NomeFantasia para exibição se logado
            }

            return View(livro); // Retornar a view com os detalhes do livro
        }

        public async Task<IActionResult> Editar(int id)
        {
            var usuarioLogado = _sessaoService.BuscarSessao();
            if (usuarioLogado == null)
            {
                ViewBag.UsuarioLogado = false; // Define que o usuário não está logado
                return Unauthorized(); // Caso a sessão esteja expirada ou inválida
            }

            // Caso o usuário esteja logado, definimos que ele está logado
            ViewBag.UsuarioLogado = true;

            var livro = await _livroInterface.GetLivroPorId(id);
            return View(livro);
        }

        public async Task<IActionResult> Remover(int id)
        {
            var usuarioLogado = _sessaoService.BuscarSessao();
            if (usuarioLogado == null)
            {
                return Unauthorized(); // Caso a sessão esteja expirada ou inválida
            }

            await _livroInterface.RemoverLivro(id);
            return RedirectToAction("Index", "Livros");
        }

        // listar os livros cadastrados pelo usuário logado
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

        public IActionResult Exportar()
        {
            // Criação de um novo DataTable apenas com a estrutura (sem dados)
            DataTable dataTable = GetModeloEstrutura();

            using (XLWorkbook workBook = new XLWorkbook())
            {
                workBook.AddWorksheet(dataTable, "Modelo Estrutura");
                using (MemoryStream ms = new MemoryStream())
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Modelo_Estrutura.xlsx");
                }
            }
        }

        // Função para criar apenas a estrutura (cabeçalhos) do DataTable
        private DataTable GetModeloEstrutura()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Modelo";

            // Adiciona as colunas sem dados
            dataTable.Columns.Add("Titulo", typeof(string));
            dataTable.Columns.Add("Isbn", typeof(string));
            dataTable.Columns.Add("Autor", typeof(string));
            dataTable.Columns.Add("Genero", typeof(string));
            dataTable.Columns.Add("NomeEditora", typeof(string));
            dataTable.Columns.Add("AnoPublicacao", typeof(DateTime));
            dataTable.Columns.Add("DataAdd", typeof(DateTime));

            return dataTable;
        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            var usuarioLogado = _sessaoService.BuscarSessao();
            if (usuarioLogado == null)
            {
                ViewBag.UsuarioLogado = false; // Define que o usuário não está logado
                return Unauthorized(); // Caso a sessão esteja expirada ou inválida
            }

            // Caso o usuário esteja logado, definimos que ele está logado
            ViewBag.UsuarioLogado = true;

            if (ModelState.IsValid)
            {
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
            var usuarioLogado = _sessaoService.BuscarSessao();
            if (usuarioLogado == null)
            {
                return Unauthorized(); // Caso a sessão esteja expirada ou inválida
            }

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
