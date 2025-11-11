using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;

namespace GerenciadorDeCertificadosApp.Domain.Interfaces.Services
{
    public interface IAtividadesDomainService
    {
        AtividadeResponseDto? CriarAtividade(AtividadeRequestDto request);

        AtividadeResponseDto? AlterarDadosAtividade(Guid id, AtividadeRequestDto request);

        void ExcluirAtividade(Guid id);

        AtividadeResponseDto? BuscarAtividadePorId(Guid id);

        List<AtividadeResponseDto>? ListarAtividades();
    }
}
