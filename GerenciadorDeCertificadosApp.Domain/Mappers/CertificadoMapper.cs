using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Entities;

namespace GerenciadorDeCertificadosApp.Domain.Mappers
{
    public static class CertificadoMapper
    {
        public static CertificadoResponseDto MapToResponseDto(this Certificado certificado)
        {
            return new CertificadoResponseDto
            {
                Id = certificado.Id,
                Nome = certificado.Nome,
                DataEmissao = certificado.DataEmissao,
                UsuarioQueGerou = certificado.Usuario is null ? null : certificado.Usuario.NomeUsuario,
                Atividades = certificado.Atividades?.Select(ca => new AtividadeResponseDto
                {
                    Id = ca.Atividade.Id,
                    Nome = ca.Atividade.Nome
                }).ToList()
            };
        }

    }
}
