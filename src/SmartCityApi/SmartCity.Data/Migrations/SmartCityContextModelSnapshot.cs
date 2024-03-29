﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SmartCity.Data.Context;
using System;

namespace SmartCity.Data.Migrations
{
    [DbContext(typeof(SmartCityContext))]
    partial class SmartCityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartCity.Domain.Models.Cidades.Cidade", b =>
                {
                    b.Property<int>("CidadeId");

                    b.Property<string>("CEP");

                    b.Property<string>("IBGE");

                    b.Property<string>("Nome");

                    b.Property<string>("UF");

                    b.HasKey("CidadeId");

                    b.ToTable("Cidades");
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Ocorrencias.MotivoDenuncia", b =>
                {
                    b.Property<int>("MotivoDenunciaId");

                    b.Property<string>("Descricao");

                    b.HasKey("MotivoDenunciaId");

                    b.ToTable("MotivosDenuncia");
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Ocorrencias.Ocorrencia", b =>
                {
                    b.Property<int>("OcorrenciaId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Atendida");

                    b.Property<DateTime>("DataHoraInclusao");

                    b.Property<string>("Descricao");

                    b.Property<string>("EnderecoCompleto");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<int>("UsuarioId");

                    b.HasKey("OcorrenciaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Ocorrencias");
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Ocorrencias.OcorrenciaImagem", b =>
                {
                    b.Property<int>("OcorrenciaImagemId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Conteudo");

                    b.Property<DateTime>("DataHoraInclusao");

                    b.Property<string>("Extensao");

                    b.Property<int>("OcorrenciaId");

                    b.HasKey("OcorrenciaImagemId");

                    b.HasIndex("OcorrenciaId");

                    b.ToTable("OcorrenciaImagens");
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Ocorrencias.VotoOcorrencia", b =>
                {
                    b.Property<int>("VotoOcorrenciaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comentario");

                    b.Property<DateTime>("DataHoraInclusao");

                    b.Property<int>("MotivoDenunciaId");

                    b.Property<int>("OcorrenciaId");

                    b.Property<bool>("Positivo");

                    b.Property<int>("UsuarioId");

                    b.HasKey("VotoOcorrenciaId");

                    b.HasIndex("OcorrenciaId");

                    b.ToTable("OcorrenciaVotos");
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Usuarios.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CidadeId");

                    b.Property<DateTime>("DataHoraCadastro");

                    b.Property<string>("Email");

                    b.Property<string>("NomeCompleto");

                    b.Property<string>("Senha");

                    b.HasKey("UsuarioId");

                    b.HasIndex("CidadeId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Ocorrencias.Ocorrencia", b =>
                {
                    b.HasOne("SmartCity.Domain.Models.Usuarios.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Ocorrencias.OcorrenciaImagem", b =>
                {
                    b.HasOne("SmartCity.Domain.Models.Ocorrencias.Ocorrencia")
                        .WithMany("Imagens")
                        .HasForeignKey("OcorrenciaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Ocorrencias.VotoOcorrencia", b =>
                {
                    b.HasOne("SmartCity.Domain.Models.Ocorrencias.Ocorrencia")
                        .WithMany("Votos")
                        .HasForeignKey("OcorrenciaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartCity.Domain.Models.Usuarios.Usuario", b =>
                {
                    b.HasOne("SmartCity.Domain.Models.Cidades.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
