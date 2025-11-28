namespace GerenciadorDeCertificadosApp.Domain.DTOs.Responses
{
    public class UsuarioResponseDto
    {
        public Guid Id { get; set; }

        public string NomeUsuario { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Perfil { get; set; } = string.Empty;
    }
}
