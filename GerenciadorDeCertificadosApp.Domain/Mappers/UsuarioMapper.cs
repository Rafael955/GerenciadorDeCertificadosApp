using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Enums;
using GerenciadorDeCertificadosApp.Domain.Extensions;
using GerenciadorDeCertificadosApp.Domain.Helpers;

namespace GerenciadorDeCertificadosApp.Domain.Mappers
{
    public static class UsuarioMapper
    {
        public static dynamic MapToResponseDto(this Usuario usuario, OperacaoUsuario operacao)
        {
            switch (operacao)
            {
                case OperacaoUsuario.RegisterUser:
                    {
                        return new RegistrarUsuarioResponseDto
                        {
                            Id = usuario.Id,
                            NomeUsuario = usuario.NomeUsuario,
                            Email = usuario.Email,
                            Perfil = usuario.Perfil.GetDescription()
                        };
                    }
                case OperacaoUsuario.AuthenticateUser:
                    {
                        return new AutenticarUsuarioResponseDto
                        {
                            Id = usuario.Id,
                            NomeUsuario = usuario.NomeUsuario,
                            Email = usuario.Email,
                            Perfil = usuario.Perfil.GetDescription(),
                            DataHoraAcesso = DateTime.Now,
                            Token = JwtTokenHelper.GenerateToken(usuario.Email, usuario.Perfil.GetDescription())
                        };
                    }
                case OperacaoUsuario.ListingUsers:
                    {
                        return new UsuarioResponseDto
                        {
                            Id = usuario.Id,
                            NomeUsuario = usuario.NomeUsuario,
                            Email = usuario.Email,
                            Perfil = usuario.Perfil.GetDescription()
                        };
                    }
                default:
                    throw new ApplicationException("Operação de usuário inválida");
            }
        }
    }
}
