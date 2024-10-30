﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewRepository.Models
{
    public class LivroModel
    {
        [Required]
        public int Id { get; set; }

        public string Capa { get; set; } = string.Empty;

        [Required]
        public string Isbn { get; set; } = string.Empty; // Código ISBN do livro

        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string AnoPublicacao { get; set; } = string.Empty;
        public string NomeEditatora { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string FonteCadastro { get; set; }

        public int UsuarioId { get; set; } // Referência à biblioteca (instituição que cadastrou)
        public UsuarioModel Usuario { get; set; } // Navegação para o usuário/instituição

        // Lista de relações com InstituicaoLivroModel
        public ICollection<InstituicaoLivroModel> InstituicaoLivros { get; set; } = new List<InstituicaoLivroModel>();
    }
}
