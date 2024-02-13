using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PechinchaMarket.Migrations
{
    /// <inheritdoc />
    public partial class listaProdutosdetalhe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_ListaProdutos_ListaProdutosId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_ListaProdutosId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "ListaProdutosId",
                table: "Produto");

            migrationBuilder.CreateTable(
                name: "DetalheListaProd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    ListaProdutosId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProdutoId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalheListaProd", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalheListaProd");

            migrationBuilder.AddColumn<Guid>(
                name: "ListaProdutosId",
                table: "Produto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_ListaProdutosId",
                table: "Produto",
                column: "ListaProdutosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_ListaProdutos_ListaProdutosId",
                table: "Produto",
                column: "ListaProdutosId",
                principalTable: "ListaProdutos",
                principalColumn: "Id");
        }
    }
}
