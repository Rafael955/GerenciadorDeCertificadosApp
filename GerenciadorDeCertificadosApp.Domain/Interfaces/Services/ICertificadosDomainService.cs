using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;

namespace GerenciadorDeCertificadosApp.Domain.Interfaces.Services
{
    public interface ICertificadosDomainService
    {
        CertificadoResponseDto? CriarCertificado(CertificadoRequestDto request);

        CertificadoResponseDto? AlterarDadosCertificado(Guid id, CertificadoRequestDto request);

        void ExcluirCertificado(Guid id);

        CertificadoResponseDto? BuscarCertificadoPorId(Guid id);

        List<CertificadoResponseDto>? ListarCertificados();
    }
}
