
using Microsoft.EntityFrameworkCore;
using NewRepository.Models;
using NewRepository.Services;
using OfficeOpenXml;
using System;
using System.ComponentModel;

namespace ImportarPlanilhaExcelProjeto.Services
{
    public class ExcelService : IExcelInterface
    {
        private readonly Contexto _context;

        public ExcelService(Contexto context)
        {
            _context = context;
        }

        public MemoryStream LerStream(IFormFile formFile)
        {
            using (var stream = new MemoryStream())
            {
                formFile?.CopyTo(stream);
                var ListaBytes = stream.ToArray();

                return new MemoryStream(ListaBytes);
            }
        }

        public List<LivroModel> LerXls(MemoryStream stream, int usuarioLogadoId)
        {
            try
            {
                var resposta = new List<LivroModel>();

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int numeroLinhas = worksheet.Dimension.End.Row;

                    for (int linha = 2; linha <= numeroLinhas; linha++)
                    {
                        var produto = new LivroModel();

                        // Verifica se as células obrigatórias têm valores
                        if (worksheet.Cells[linha, 1].Value != null &&
                            worksheet.Cells[linha, 2].Value != null &&
                            worksheet.Cells[linha, 3].Value != null &&
                            worksheet.Cells[linha, 4].Value != null)
                        {
                            // Lê os dados da planilha Excel
                            produto.Isbn = worksheet.Cells[linha, 1].Value.ToString(); // ISBN
                            produto.Titulo = worksheet.Cells[linha, 2].Value.ToString(); // TítulO
                            produto.Genero = worksheet.Cells[linha, 3].Value?.ToString(); // Gênero
                            produto.AnoPublicacao = worksheet.Cells[linha, 4].Value?.ToString(); // Ano de publicação
                            produto.Autor = worksheet.Cells[linha, 5].Value.ToString(); // Autor
                            produto.NomeEditatora = worksheet.Cells[linha, 6].Value?.ToString(); // Editora                                                            
                            if (worksheet.Cells[linha, 7].Value != null)
                            {
                                string dataString = worksheet.Cells[linha, 7].Value.ToString();
                                DateTime dataAdd;

                                // Adiciona log para verificar o valor antes da conversão
                                Console.WriteLine($"Valor da data na linha {linha}: {dataString}");

                                if (DateTime.TryParseExact(dataString, "dd/MM/yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None,
                                    out dataAdd))
                                {
                                    produto.DataAdd = dataAdd; // Data de adição
                                }
                                else
                                {
                                    throw new Exception($"Data em formato inválido na linha {linha}. Esperado formato: DD/MM/YYYY. Valor lido: {dataString}");
                                }
                            }
                            produto.QtdLivro = Convert.ToInt32(worksheet.Cells[linha, 8].Value); // Quantidade




                            // Lê a data de adição e a converte para DateTime
                            if (worksheet.Cells[linha, 8].Value != null)
                            {
                                DateTime dataAdd;
                                if (DateTime.TryParseExact(worksheet.Cells[linha, 8].Value.ToString(), "dd/MM/yyyy",
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None,
                                    out dataAdd))
                                {
                                    produto.DataAdd = dataAdd; // Data de adição
                                }
                                else
                                {
                                    throw new Exception($"Data em formato inválido na linha {linha}. Esperado formato: DD/MM/YYYY.");
                                }
                            }


                            // Atribui o ID do usuário logado
                            produto.UsuarioId = usuarioLogadoId;

                            // Adiciona o livro à lista de resposta
                            resposta.Add(produto);
                        }
                    }
                }

                return resposta;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler o arquivo Excel: " + ex.Message);
            }
        }




        public void SalvarDados(List<LivroModel> livros, int usuarioId)
        {
            try
            {
                foreach (var livro in livros)
                {
                    // Definindo o usuário associado ao livro
                    livro.UsuarioId = usuarioId;

                    // Garantindo que o campo FonteCadastro tenha um valor
                    if (string.IsNullOrEmpty(livro.FonteCadastro))
                    {
                        livro.FonteCadastro = "Importado"; // Define um valor padrão
                    }

                    // Adiciona o livro ao contexto do banco de dados
                    _context.Add(livro);

                    // Salva as mudanças no banco de dados
                    _context.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Captura exceções relacionadas à atualização do banco de dados
                var innerExceptionMessage = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                throw new Exception($"Erro ao salvar os dados: {innerExceptionMessage}");
            }
            catch (Exception ex)
            {
                // Lança exceções genéricas
                throw new Exception("Erro ao salvar os dados: " + ex.Message);
            }
        }

    }
}