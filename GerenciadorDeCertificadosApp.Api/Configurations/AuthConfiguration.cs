using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GerenciadorDeCertificadosApp.Api.Configurations
{
    public static class AuthConfiguration
    {
        public static void AddAuthConfiguration(this IServiceCollection services)
        {
            var jwtSecret = "D66506D8-E285-4040-9036-17C2789E06E3";
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            //Criando a configuração para autenticação com JWT - JSON WEB TOKEN
            services.AddAuthentication(
                auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(
                    bearer =>
                    {
                        bearer.RequireHttpsMetadata = false;
                        bearer.SaveToken = true;
                        bearer.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = signingKey,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            // ajustar a janela de tolerância se necessário
                            ClockSkew = TimeSpan.FromMinutes(5)
                        };

                        // logs e debugging de falhas de autenticação
                        bearer.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                var logger = context.HttpContext.RequestServices
                                    .GetRequiredService<ILoggerFactory>()
                                    .CreateLogger("JwtAuth");
                                logger.LogError(context.Exception, "Falha na autenticação JWT: {Message}", context.Exception.Message);
                                return Task.CompletedTask;
                            }
                        };
                    });

        }
    }
}
