using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Entities;

namespace GerenciadorDeCertificadosApp.Domain.Mappers
{
    public static class AtividadeMapper
    {
        public static AtividadeResponseDto MapToResponseDto(this Atividade atividade)
        {
            return new AtividadeResponseDto
            {
                Id = atividade.Id,
                Nome = atividade.Nome
            };
        }
    }
}
