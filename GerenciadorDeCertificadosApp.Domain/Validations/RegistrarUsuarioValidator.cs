using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;

namespace GerenciadorDeCertificadosApp.Domain.Validations
{
    public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioRequestDto>
    {
        public RegistrarUsuarioValidator()
        {
            RuleFor(u => u.NomeUsuario)
                .NotEmpty().WithMessage("O nome de usuário deve ser informado.")
                .MaximumLength(100).WithMessage("O nome de usuário deve ter no máximo 50 caracteres.")
                .MinimumLength(3).WithMessage("O nome de usuário deve ter no mínimo 3 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O email do usuário deve ser informado.")
                .MaximumLength(100).WithMessage("O email do usuário deve ter no máximo 100 caracteres.")
                .EmailAddress().WithMessage("O email do usuário deve ser um endereço de email válido.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A senha do usuário deve ser informada.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$").WithMessage("A senha deve ter pelo menos 1 letra minúscula, 1 letra maiúscula, 1 número, 1 símbolo e no mínimo 8 caracteres.")
                .MaximumLength(20).WithMessage("A senha do usuário deve ter no máximo 20 caracteres.");

            RuleFor(u => u.Perfil)
                .InclusiveBetween(1, 2).WithMessage("O perfil do usuário informado é inválido!");
        }
    }
}
