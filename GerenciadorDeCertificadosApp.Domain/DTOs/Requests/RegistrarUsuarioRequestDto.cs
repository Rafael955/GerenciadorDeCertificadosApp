namespace GerenciadorDeCertificadosApp.Domain.DTOs.Requests
{
    public class RegistrarUsuarioRequestDto
    {
        public string NomeUsuario { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public int Perfil { get; set; }
    }
}
