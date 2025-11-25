using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Enums;
using GerenciadorDeCertificadosApp.Domain.Exceptions;
using GerenciadorDeCertificadosApp.Domain.Helpers;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Services;
using GerenciadorDeCertificadosApp.Domain.Mappers;
using GerenciadorDeCertificadosApp.Domain.Validations;

namespace GerenciadorDeCertificadosApp.Domain.Services
{
    public class UsuariosDomainService : IUsuariosDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosDomainService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public RegistrarUsuarioResponseDto? RegistrarUsuario(RegistrarUsuarioRequestDto request)
        {
            var validation = new RegistrarUsuarioValidator().Validate(request);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            if (_usuarioRepository.Any(request.Email))
                throw new ApplicationException("Já existe um usuário cadastrado com este e-mail.");

            var usuario = new Usuario(request.NomeUsuario, request.Email, CryptoHelper.GetSHA256(request.Senha), request.Perfil);

            _usuarioRepository.Add(usuario);

            return usuario.MapToResponseDto(OperacaoUsuario.RegisterUser);
        }

        public AutenticarUsuarioResponseDto? AutenticarUsuario(AutenticarUsuarioRequestDto request)
        {
            var validation = new AutenticarUsuarioValidator().Validate(request);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var usuario = _usuarioRepository.GetUserByEmailAndPassword(request.Email, CryptoHelper.GetSHA256(request.Senha));

            if(usuario == null)
                throw new UserNotFoundException("Usuário ou senha inválidos.");

            return usuario.MapToResponseDto(OperacaoUsuario.AuthenticateUser);
        }
    }
}
