using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;

namespace GerenciadorDeCertificadosApp.Domain.Validations
{
    public class CertificadoValidator : AbstractValidator<CertificadoRequestDto>
    {
        public CertificadoValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do aluno para o certificado deve ser informado.")
                .MaximumLength(150).WithMessage("O nome do aluno para o certificado deve ter no máximo 150 caracteres.")
                .MinimumLength(3).WithMessage("O nome do aluno para o certificado deve ter no mínimo 3 caracteres.");

            RuleFor(c => c.Atividades.Count)
                .NotEmpty().WithMessage("As atividades do aluno para o certificado devem ser informadas.");

            RuleFor(c => c.UsuarioId)
                .NotEmpty().WithMessage("O id do usuário deve ser informado.");
        }
    }
}
