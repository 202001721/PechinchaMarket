using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PechinchaMarket.Migrations
{
    /// <inheritdoc />
    public partial class descontos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountDuration",
                table: "ProdutoLoja",
                newName: "StartDiscount");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDiscount",
                table: "ProdutoLoja",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Preferencias",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDiscount",
                table: "ProdutoLoja");

            migrationBuilder.RenameColumn(
                name: "StartDiscount",
                table: "ProdutoLoja",
                newName: "DiscountDuration");

            migrationBuilder.AlterColumn<string>(
                name: "Preferencias",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
