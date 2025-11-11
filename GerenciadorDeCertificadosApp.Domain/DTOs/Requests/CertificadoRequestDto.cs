namespace GerenciadorDeCertificadosApp.Domain.DTOs.Requests
{
    public class CertificadoRequestDto
    {
        public string Nome { get; set; } = string.Empty;

        public List<AtividadeRequestDto> Atividades { get; set; } = new List<AtividadeRequestDto>();

        public Guid UsuarioId { get; set; }
    }
}
