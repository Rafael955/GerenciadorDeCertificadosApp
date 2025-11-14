namespace GerenciadorDeCertificadosApp.Api.Configurations
{
    public static class CorsConfiguration
    {
        public const string PolicyName = "AllowAngularApp";

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, builder =>
                {
                    builder.WithOrigins(
                            "http://localhost:4200",
                            "https://localhost:4200",
                            "http://127.0.0.1:4200",
                            "https://127.0.0.1:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app) 
        {
            //Habilitando o CORS
            app.UseCors(PolicyName);
            return app;
        }
    }
}
