using GerenciadorDeCertificadosApp.Domain.Enums;
using GerenciadorDeCertificadosApp.Domain.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace GerenciadorDeCertificadosApp.Tests.Handlers
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder) : base(options, logger, encoder)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Cria um ClaimsPrincipal fixo representando um admin
            var claims = new[]
            {
                //new Claim(ClaimTypes.NameIdentifier, "07146c5b-511f-4fdf-99e7-ccb72105922c"),
                new Claim(ClaimTypes.Name, "admin@admin.com"),
                new Claim(ClaimTypes.Role, Perfil.Administrador.GetDescription()),
                //new Claim("perfil", "Administrador")
            };

            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}