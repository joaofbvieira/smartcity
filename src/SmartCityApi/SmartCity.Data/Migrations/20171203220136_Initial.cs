using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SmartCity.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    CidadeId = table.Column<int>(type: "int", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IBGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.CidadeId);
                });

            migrationBuilder.CreateTable(
                name: "MotivosDenuncia",
                columns: table => new
                {
                    MotivoDenunciaId = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosDenuncia", x => x.MotivoDenunciaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CidadeId = table.Column<int>(type: "int", nullable: false),
                    DataHoraCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Cidades_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidades",
                        principalColumn: "CidadeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencias",
                columns: table => new
                {
                    OcorrenciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Atendida = table.Column<bool>(type: "bit", nullable: false),
                    DataHoraInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencias", x => x.OcorrenciaId);
                    table.ForeignKey(
                        name: "FK_Ocorrencias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcorrenciaImagens",
                columns: table => new
                {
                    OcorrenciaImagemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Conteudo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DataHoraInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Extensao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OcorrenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcorrenciaImagens", x => x.OcorrenciaImagemId);
                    table.ForeignKey(
                        name: "FK_OcorrenciaImagens_Ocorrencias_OcorrenciaId",
                        column: x => x.OcorrenciaId,
                        principalTable: "Ocorrencias",
                        principalColumn: "OcorrenciaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OcorrenciaVotos",
                columns: table => new
                {
                    VotoOcorrenciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataHoraInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MotivoDenunciaId = table.Column<int>(type: "int", nullable: false),
                    OcorrenciaId = table.Column<int>(type: "int", nullable: false),
                    Positivo = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OcorrenciaVotos", x => x.VotoOcorrenciaId);
                    table.ForeignKey(
                        name: "FK_OcorrenciaVotos_Ocorrencias_OcorrenciaId",
                        column: x => x.OcorrenciaId,
                        principalTable: "Ocorrencias",
                        principalColumn: "OcorrenciaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciaImagens_OcorrenciaId",
                table: "OcorrenciaImagens",
                column: "OcorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencias_UsuarioId",
                table: "Ocorrencias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_OcorrenciaVotos_OcorrenciaId",
                table: "OcorrenciaVotos",
                column: "OcorrenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CidadeId",
                table: "Usuarios",
                column: "CidadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotivosDenuncia");

            migrationBuilder.DropTable(
                name: "OcorrenciaImagens");

            migrationBuilder.DropTable(
                name: "OcorrenciaVotos");

            migrationBuilder.DropTable(
                name: "Ocorrencias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Cidades");
        }
    }
}
