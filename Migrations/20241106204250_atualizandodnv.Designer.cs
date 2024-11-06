﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewRepository.Models;

#nullable disable

namespace NewRepository.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20241106204250_atualizandodnv")]
    partial class atualizandodnv
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("NewRepository.Models.Biblioteca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Bibliotecas");
                });

            modelBuilder.Entity("NewRepository.Models.InstituicaoLivroModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LivroId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LivroId");

                    b.HasIndex("UsuarioId", "LivroId")
                        .IsUnique();

                    b.ToTable("InstituicaoLivros");
                });

            modelBuilder.Entity("NewRepository.Models.LivroModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnoPublicacao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Capa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FonteCadastro")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeEditatora")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Isbn")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("NewRepository.Models.UsuarioModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("SenhaHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("SenhaSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Instituicoes");
                });

            modelBuilder.Entity("NewRepository.Models.InstituicaoLivroModel", b =>
                {
                    b.HasOne("NewRepository.Models.LivroModel", "Livro")
                        .WithMany("InstituicaoLivros")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewRepository.Models.UsuarioModel", "Usuario")
                        .WithMany("InstituicaoLivros")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livro");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("NewRepository.Models.LivroModel", b =>
                {
                    b.HasOne("NewRepository.Models.UsuarioModel", "Usuario")
                        .WithMany("Livros")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("NewRepository.Models.LivroModel", b =>
                {
                    b.Navigation("InstituicaoLivros");
                });

            modelBuilder.Entity("NewRepository.Models.UsuarioModel", b =>
                {
                    b.Navigation("InstituicaoLivros");

                    b.Navigation("Livros");
                });
#pragma warning restore 612, 618
        }
    }
}
