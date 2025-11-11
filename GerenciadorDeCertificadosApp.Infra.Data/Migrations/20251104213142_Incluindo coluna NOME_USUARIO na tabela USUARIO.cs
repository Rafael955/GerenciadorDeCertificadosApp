using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeCertificadosApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncluindocolunaNOME_USUARIOnatabelaUSUARIO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeUsuario",
                table: "USUARIO",
                newName: "NOME_USUARIO");

            migrationBuilder.AlterColumn<string>(
                name: "NOME_USUARIO",
                table: "USUARIO",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NOME_USUARIO",
                table: "USUARIO",
                newName: "NomeUsuario");

            migrationBuilder.AlterColumn<string>(
                name: "NomeUsuario",
                table: "USUARIO",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
