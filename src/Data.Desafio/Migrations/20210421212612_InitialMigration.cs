using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Desafio.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    NomeFantasia = table.Column<string>(type: "varchar(255)", nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(14)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(255)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(30)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(150)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(255)", nullable: false),
                    Uf = table.Column<string>(type: "varchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    ValorCompra = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(nullable: false),
                    LojaId = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    UltimaAtualizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoques_Lojas_LojaId",
                        column: x => x.LojaId,
                        principalTable: "Lojas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estoques_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_LojaId",
                table: "Estoques",
                column: "LojaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
