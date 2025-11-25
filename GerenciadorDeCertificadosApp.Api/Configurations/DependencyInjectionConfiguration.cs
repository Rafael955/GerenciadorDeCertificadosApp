using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Services;
using GerenciadorDeCertificadosApp.Domain.Services;
using GerenciadorDeCertificadosApp.Infra.Data.Repositories;

namespace GerenciadorDeCertificadosApp.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            //Configuração para injeção de dependência
            services.AddScoped<IAtividadesRepository, AtividadesRepository>();
            services.AddScoped<ICertificadoAtividadesRepository, CertificadoAtividadesRepository>();
            services.AddScoped<ICertificadosRepository, CertificadosRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<ICertificadosDomainService, CertificadosDomainService>();
            services.AddScoped<IAtividadesDomainService, AtividadesDomainService>();
            services.AddScoped<IUsuariosDomainService, UsuariosDomainService>();
        }
    }
}
