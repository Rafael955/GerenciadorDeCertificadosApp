using System.Text.Json.Serialization;

namespace GerenciadorDeCertificadosApp.Domain.DTOs.Responses
{
    public class ErrorMessageResponseDto
    {
        public List<string> ErrorMessages { get; private set; }

        // Construtor padrão necessário para desserialização
        public ErrorMessageResponseDto()
        {
            ErrorMessages = new List<string>();
        }

        public ErrorMessageResponseDto(string message)
        {
            ErrorMessages = [message];
        }

        public ErrorMessageResponseDto(List<string> messages)
        {
            ErrorMessages = messages ?? new List<string>();
        }
    }
}
