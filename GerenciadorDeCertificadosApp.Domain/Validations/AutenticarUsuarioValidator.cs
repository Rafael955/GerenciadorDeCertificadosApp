using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;

namespace GerenciadorDeCertificadosApp.Domain.Validations
{
    public class AutenticarUsuarioValidator : AbstractValidator<AutenticarUsuarioRequestDto>
    {
        public AutenticarUsuarioValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O email do usuário deve ser informado.")
                .MaximumLength(100).WithMessage("O email do usuário deve ter no máximo 100 caracteres.")
                .EmailAddress().WithMessage("O email do usuário deve ser um endereço de email válido.");
            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A senha do usuário deve ser informada.")
                .MaximumLength(20).WithMessage("A senha do usuário deve ter no máximo 20 caracteres.");
        }
    }
}
