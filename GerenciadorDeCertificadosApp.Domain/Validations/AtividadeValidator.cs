using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;

namespace GerenciadorDeCertificadosApp.Domain.Validations
{
    public class AtividadeValidator : AbstractValidator<AtividadeRequestDto>
    {
        public AtividadeValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("O nome da atividade deve ser informado.")
                .MaximumLength(100).WithMessage("O nome da atividade deve ter no máximo 100 caracteres.")
                .MinimumLength(2).WithMessage("O nome da atividade deve ter no mínimo 2 caracteres.");
        }
    }
}
