using NewRepository.Models;
using Microsoft.EntityFrameworkCore;

using System.Configuration;
using NewRepository.Services.Livro;
using NewRepository.Services.UsuarioService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<Contexto>(opcoes =>
    opcoes.UseSqlite(builder.Configuration.GetConnectionString("ConexaoSQlite")));

builder.Services.AddScoped<ILivroInterface, LivroService>();
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Redireciona para a página de erro em modo de produção
    app.UseHsts(); 
}

app.UseHttpsRedirection(); // Redireciona todas as requisições para HTTPS
app.UseStaticFiles(); // Permite arquivos estáticos (CSS, JS, imagens)

app.UseRouting(); 

app.UseAuthorization(); 


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

