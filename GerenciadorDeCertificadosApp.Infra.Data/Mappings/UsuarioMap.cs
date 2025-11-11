using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Enums;
using GerenciadorDeCertificadosApp.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeCertificadosApp.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("ID");

            builder.Property(u => u.NomeUsuario)
                .HasColumnName("NOME_USUARIO")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(u => u.Perfil)
                .HasColumnName("PERFIL")
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasData(
                new Usuario(
                    id: Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c"),
                    nomeUsuario: "Admin",
                    email: "admin@admin.com",
                    senha: "6f2cb9dd8f4b65e24e1c3f3fa5bc57982349237f11abceacd45bbcb74d621c25", //Admin@12345
                    perfil: Perfil.Administrador
                    ));
        }
    }
}
