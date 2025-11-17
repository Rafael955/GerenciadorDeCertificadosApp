using GerenciadorDeCertificadosApp.Api.Exceptions;

namespace GerenciadorDeCertificadosApp.Api.Configurations
{
    public static class GlobalExceptionsConfiguration
    {
        public static void AddGlobalExceptionsConfiguration(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();
        }

        public static void UseGlobalExceptionsConfiguration(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
