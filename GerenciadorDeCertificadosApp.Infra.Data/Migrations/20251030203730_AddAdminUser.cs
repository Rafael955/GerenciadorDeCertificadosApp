using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeCertificadosApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeUsuario",
                table: "USUARIO",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "USUARIO",
                columns: new[] { "Id", "EMAIL", "NomeUsuario", "PERFIL", "SENHA" },
                values: new object[] { new Guid("07146c5b-511f-4fdf-99e7-ccb72105922c"), "admin@admin.com", "Admin", 1, "6f2cb9dd8f4b65e24e1c3f3fa5bc57982349237f11abceacd45bbcb74d621c25" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "Id",
                keyValue: new Guid("07146c5b-511f-4fdf-99e7-ccb72105922c"));

            migrationBuilder.DropColumn(
                name: "NomeUsuario",
                table: "USUARIO");
        }
    }
}
