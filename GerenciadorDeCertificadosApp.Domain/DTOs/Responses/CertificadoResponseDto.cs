namespace GerenciadorDeCertificadosApp.Domain.DTOs.Responses
{
    public class CertificadoResponseDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public List<AtividadeResponseDto>? Atividades { get; set; }

        public DateTime DataEmissao { get; set; }

        public string? UsuarioQueGerou { get; set; } = string.Empty;
    }
}
