using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using GerenciadorDeCertificadosApp.Tests.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorDeCertificadosApp.Tests.Factories
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // 1. Remove DbContextOptions e o DbContext
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));
                if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

                var dataContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DataContext));
                if (dataContextDescriptor != null) services.Remove(dataContextDescriptor);

                // 2. Registra o SQL Server para Teste
                // USE UMA STRING DE CONEXÃO EXCLUSIVA PARA TESTES!
                services.AddDbContext<DataContext>(options =>
                {
                    // Exemplo: Conectar a uma instância LocalDB com nome de banco exclusivo
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GerenciadorCertificados_TEST;Trusted_Connection=True;MultipleActiveResultSets=true");
                });

                // 3. (Opcional) Inicialização do Banco de Dados
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DataContext>();

                    // Garante que o banco de dados exista e execute as Migrations
                    db.Database.Migrate();
                }

                // Registra um esquema de autenticação de teste e o torna padrão
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
            });
        }

        // NOVO: Método público e assíncrono para apagar o banco de dados
        public async Task ResetDatabaseAsync()
        {
            // 1. Crie um novo escopo de serviço
            using (var scope = Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();

                // 2. Exclua o banco de dados de teste (Assíncrono)
                await db.Database.EnsureDeletedAsync();
            }
        }
    }

}