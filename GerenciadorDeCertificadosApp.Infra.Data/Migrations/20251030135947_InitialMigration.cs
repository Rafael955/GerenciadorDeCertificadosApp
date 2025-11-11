using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorDeCertificadosApp.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ATIVIDADE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATIVIDADE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SENHA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PERFIL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CERTIFICADO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DATA_EMISSAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USUARIO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CERTIFICADO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CERTIFICADO_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CERTIFICADO_ATIVIDADE",
                columns: table => new
                {
                    CERTIFICADO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ATIVIDADE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CERTIFICADO_ATIVIDADE", x => new { x.CERTIFICADO_ID, x.ATIVIDADE_ID });
                    table.ForeignKey(
                        name: "FK_CERTIFICADO_ATIVIDADE_ATIVIDADE_ATIVIDADE_ID",
                        column: x => x.ATIVIDADE_ID,
                        principalTable: "ATIVIDADE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CERTIFICADO_ATIVIDADE_CERTIFICADO_CERTIFICADO_ID",
                        column: x => x.CERTIFICADO_ID,
                        principalTable: "CERTIFICADO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CERTIFICADO_USUARIO_ID",
                table: "CERTIFICADO",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CERTIFICADO_ATIVIDADE_ATIVIDADE_ID",
                table: "CERTIFICADO_ATIVIDADE",
                column: "ATIVIDADE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_EMAIL",
                table: "USUARIO",
                column: "EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CERTIFICADO_ATIVIDADE");

            migrationBuilder.DropTable(
                name: "ATIVIDADE");

            migrationBuilder.DropTable(
                name: "CERTIFICADO");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
