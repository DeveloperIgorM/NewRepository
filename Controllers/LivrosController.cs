using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewRepository.Dto;
using NewRepository.Filtros;
using NewRepository.Models;
using NewRepository.Services;
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
        private readonly IExcelInterface _excelInterface;

        public LivrosController(ILivroInterface livroInterface, ISessaoInterface sessaoService, Contexto context, IExcelInterface excelInterface)
        {
            _livroInterface = livroInterface;
            _sessaoService = sessaoService;
            _context = context;
            _excelInterface = excelInterface;
        }

        [UsuarioLogado]
        public async Task<IActionResult> Index()
        {
            var usuarioLogado = _sessaoService.BuscarSessao();
            List<LivroModel> livros;

            if (usuarioLogado != null)
            {
                ViewBag.NomeFantasia = usuarioLogado.NomeFantasia; // Passa o NomeFantasia para a ViewBag
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

        public async Task<IActionResult> Detalhes(int id)
        {
            var livro = await _livroInterface.GetLivroPorId(id);

            if (livro == null)
            {
                return NotFound();
            }

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

            ViewBag.UsuarioLogado = true; // Define que o usuário está logado
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

        private DataTable GetModeloEstrutura()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Modelo";

            // Adiciona as colunas sem dados
            dataTable.Columns.Add("Isbn", typeof(string));
            dataTable.Columns.Add("Titulo", typeof(string));
            dataTable.Columns.Add("Genero", typeof(string));
            dataTable.Columns.Add("AnoPublicacao", typeof(DateTime));
            dataTable.Columns.Add("Autor", typeof(string));
            dataTable.Columns.Add("NomeEditora", typeof(string));
            dataTable.Columns.Add("DataAdd", typeof(DateTime));
            dataTable.Columns.Add("QtdLivro", typeof(int));
            

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

            ViewBag.UsuarioLogado = true; // Define que o usuário está logado

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
        [HttpPost]
        public IActionResult ImportExcel(IFormFile form)
        {
            // Recupera a sessão do usuário logado
            var usuarioLogado = _sessaoService.BuscarSessao();

            // Verifica se o usuário está logado, caso contrário retorna não autorizado
            if (usuarioLogado == null)
            {
                return Unauthorized(); // Sessão expirada ou inválida
            }

            // Verifica se o estado do Model está válido
            if (ModelState.IsValid)
            {
                // Lê o arquivo Excel em um stream
                var streamFile = _excelInterface.LerStream(form);

                // Lê os dados do Excel e passa o ID do usuário logado para o método LerXls
                var livros = _excelInterface.LerXls(streamFile, usuarioLogado.Id); // Corrige para usar 'Id' do usuário logado

                // Salva os dados dos livros e passa o ID do usuário
              _excelInterface.SalvarDados(livros, usuarioLogado.Id); // Passa o ID do usuário corretamente

                // Redireciona para a página principal após a importação bem-sucedida
                return RedirectToAction("Index");
            }
            else
            {
                // Caso haja erro de validação, redireciona para a página principal
                return RedirectToAction("Index");
            }
        }

    }
}
