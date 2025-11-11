using GerenciadorDeCertificadosApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeCertificadosApp.Infra.Data.Mappings
{
    public class CertificadoMap : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.ToTable("CERTIFICADO");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("ID");

            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.DataEmissao)
                .HasColumnName("DATA_EMISSAO")
                .IsRequired();

            builder.Property(c => c.UsuarioId)
                .HasColumnName("USUARIO_ID")
                .IsRequired();

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Certificados)
                .HasForeignKey(c => c.UsuarioId);
        }
    }
}
