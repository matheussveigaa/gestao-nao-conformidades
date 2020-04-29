using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GestaoDeNaoConformidades.Migrations
{
    public partial class addtableacaocorretiva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcaoCorretiva",
                columns: table => new
                {
                    AcaoCorretivaID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NaoConformidadeID = table.Column<long>(nullable: false),
                    OqueFazer = table.Column<string>(maxLength: 50, nullable: true),
                    PorqueFazer = table.Column<string>(nullable: true),
                    ComoFazer = table.Column<string>(nullable: true),
                    OndeFazer = table.Column<string>(maxLength: 30, nullable: true),
                    AteQuando = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcaoCorretiva", x => x.AcaoCorretivaID);
                    table.ForeignKey(
                        name: "FK_AcaoCorretiva_NaoConformidade_NaoConformidadeID",
                        column: x => x.NaoConformidadeID,
                        principalTable: "NaoConformidade",
                        principalColumn: "NaoConformidadeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcaoCorretiva_NaoConformidadeID",
                table: "AcaoCorretiva",
                column: "NaoConformidadeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcaoCorretiva");
        }
    }
}
