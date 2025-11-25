namespace GerenciadorDeCertificadosApp.Domain.DTOs.Requests
{
    public class AutenticarUsuarioRequestDto
    {
        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;
    }
}
