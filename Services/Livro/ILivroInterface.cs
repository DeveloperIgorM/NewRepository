﻿using NewRepository.Dto;
using NewRepository.Models;

namespace NewRepository.Services.Livro
{
    public interface ILivroInterface
    {
        Task<LivroModel> CriarLivro(LivroCriacaoDto livroCriacaoDto, IFormFile foto, int usuarioId); // Adicione o ID do usuário
        Task<List<LivroModel>> GetLivros(int? usuarioId);
        Task<LivroModel> GetLivroPorId(int id);
        Task<LivroModel> EditarLivro(LivroModel livro, IFormFile? foto);
        Task<LivroModel> RemoverLivro(int id);
        Task<List<LivroModel>> GetLivrosFiltro(string? pesquisar);
    }
}
