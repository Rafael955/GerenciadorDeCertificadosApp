using GerenciadorDeCertificadosApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeCertificadosApp.Infra.Data.Mappings
{
    public class AtividadeMap : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.ToTable("ATIVIDADE");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("ID");

            builder.Property(a => a.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
