using GerenciadorDeCertificadosApp.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeCertificadosApp.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new AtividadeMap());
            modelBuilder.ApplyConfiguration(new CertificadoMap());
            modelBuilder.ApplyConfiguration(new CertificadoAtividadeMap());
        }   
    }
}
