namespace GerenciadorDeCertificadosApp.Domain.DTOs.Requests
{
    public class RegistrarUsuarioRequestDto
    {
        public string NomeUsuario { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public int Perfil { get; set; }
    }
}
