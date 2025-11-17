using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeCertificadosApp.Api.Configurations
{
    public static class DataContextConfiguration
    {
        public static void AddDataContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
