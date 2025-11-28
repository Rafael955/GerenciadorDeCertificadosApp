using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;

namespace GerenciadorDeCertificadosApp.Domain.Interfaces.Services
{
    public interface IUsuariosDomainService
    {
        RegistrarUsuarioResponseDto? RegistrarUsuario(RegistrarUsuarioRequestDto request);

        AutenticarUsuarioResponseDto? AutenticarUsuario(AutenticarUsuarioRequestDto request);

        List<UsuarioResponseDto>? ListarUsuarios();

        void ExcluirContaUsuario(Guid usuarioId);
    }
}
