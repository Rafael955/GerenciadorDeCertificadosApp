using GerenciadorDeCertificadosApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeCertificadosApp.Infra.Data.Mappings
{
    public class CertificadoAtividadeMap : IEntityTypeConfiguration<CertificadoAtividade>
    {
        public void Configure(EntityTypeBuilder<CertificadoAtividade> builder)
        {
            builder.ToTable("CERTIFICADO_ATIVIDADE");

            builder.HasKey(ca => new { ca.IdCertificado, ca.IdAtividade });

            builder.Property(ca => ca.IdCertificado)
                .HasColumnName("ID_CERTIFICADO")
                .IsRequired();

            builder.Property(ca => ca.IdAtividade)
                .HasColumnName("ID_ATIVIDADE")
                .IsRequired();

            builder.HasOne(ca => ca.Certificado)
                .WithMany(c => c.Atividades)
                .HasForeignKey(ca => ca.IdCertificado);

            builder.HasOne(ca => ca.Atividade)
                .WithMany(a => a.Certificados)
                .HasForeignKey(ca => ca.IdAtividade);
        }
    }
}
