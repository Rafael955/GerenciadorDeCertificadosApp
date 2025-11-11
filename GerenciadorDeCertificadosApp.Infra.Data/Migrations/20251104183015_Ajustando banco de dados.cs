using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeCertificadosApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ajustandobancodedados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CERTIFICADO_ATIVIDADE_ATIVIDADE_ATIVIDADE_ID",
                table: "CERTIFICADO_ATIVIDADE");

            migrationBuilder.DropForeignKey(
                name: "FK_CERTIFICADO_ATIVIDADE_CERTIFICADO_CERTIFICADO_ID",
                table: "CERTIFICADO_ATIVIDADE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "USUARIO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ATIVIDADE_ID",
                table: "CERTIFICADO_ATIVIDADE",
                newName: "ID_ATIVIDADE");

            migrationBuilder.RenameColumn(
                name: "CERTIFICADO_ID",
                table: "CERTIFICADO_ATIVIDADE",
                newName: "ID_CERTIFICADO");

            migrationBuilder.RenameIndex(
                name: "IX_CERTIFICADO_ATIVIDADE_ATIVIDADE_ID",
                table: "CERTIFICADO_ATIVIDADE",
                newName: "IX_CERTIFICADO_ATIVIDADE_ID_ATIVIDADE");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CERTIFICADO",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ATIVIDADE",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CERTIFICADO_ATIVIDADE_ATIVIDADE_ID_ATIVIDADE",
                table: "CERTIFICADO_ATIVIDADE",
                column: "ID_ATIVIDADE",
                principalTable: "ATIVIDADE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CERTIFICADO_ATIVIDADE_CERTIFICADO_ID_CERTIFICADO",
                table: "CERTIFICADO_ATIVIDADE",
                column: "ID_CERTIFICADO",
                principalTable: "CERTIFICADO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CERTIFICADO_ATIVIDADE_ATIVIDADE_ID_ATIVIDADE",
                table: "CERTIFICADO_ATIVIDADE");

            migrationBuilder.DropForeignKey(
                name: "FK_CERTIFICADO_ATIVIDADE_CERTIFICADO_ID_CERTIFICADO",
                table: "CERTIFICADO_ATIVIDADE");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "USUARIO",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID_ATIVIDADE",
                table: "CERTIFICADO_ATIVIDADE",
                newName: "ATIVIDADE_ID");

            migrationBuilder.RenameColumn(
                name: "ID_CERTIFICADO",
                table: "CERTIFICADO_ATIVIDADE",
                newName: "CERTIFICADO_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CERTIFICADO_ATIVIDADE_ID_ATIVIDADE",
                table: "CERTIFICADO_ATIVIDADE",
                newName: "IX_CERTIFICADO_ATIVIDADE_ATIVIDADE_ID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CERTIFICADO",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ATIVIDADE",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CERTIFICADO_ATIVIDADE_ATIVIDADE_ATIVIDADE_ID",
                table: "CERTIFICADO_ATIVIDADE",
                column: "ATIVIDADE_ID",
                principalTable: "ATIVIDADE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CERTIFICADO_ATIVIDADE_CERTIFICADO_CERTIFICADO_ID",
                table: "CERTIFICADO_ATIVIDADE",
                column: "CERTIFICADO_ID",
                principalTable: "CERTIFICADO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
