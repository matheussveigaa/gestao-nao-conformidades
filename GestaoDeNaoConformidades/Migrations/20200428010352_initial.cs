using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GestaoDeNaoConformidades.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    DepartamentoID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.DepartamentoID);
                });

            migrationBuilder.CreateTable(
                name: "NaoConformidade",
                columns: table => new
                {
                    NaoConformidadeID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(nullable: true),
                    DataOcorrencia = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaoConformidade", x => x.NaoConformidadeID);
                });

            migrationBuilder.CreateTable(
                name: "NaoConformidadeDepartamento",
                columns: table => new
                {
                    NaoConformidadeID = table.Column<long>(nullable: false),
                    DepartamentoID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaoConformidadeDepartamento", x => new { x.NaoConformidadeID, x.DepartamentoID });
                    table.ForeignKey(
                        name: "FK_NaoConformidadeDepartamento_Departamento_DepartamentoID",
                        column: x => x.DepartamentoID,
                        principalTable: "Departamento",
                        principalColumn: "DepartamentoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NaoConformidadeDepartamento_NaoConformidade_NaoConformidade~",
                        column: x => x.NaoConformidadeID,
                        principalTable: "NaoConformidade",
                        principalColumn: "NaoConformidadeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NaoConformidadeDepartamento_DepartamentoID",
                table: "NaoConformidadeDepartamento",
                column: "DepartamentoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NaoConformidadeDepartamento");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "NaoConformidade");
        }
    }
}
