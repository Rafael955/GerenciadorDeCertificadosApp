using Bogus;
using FluentAssertions;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace GerenciadorDeCertificadosApp.Tests
{
    public class UsuariosTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public UsuariosTest()
        {
            _client = new WebApplicationFactory<Program>().CreateClient();
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve criar um novo usuário com sucesso")]
        public void DeveCriarUmNovoUsuarioComSucesso()
        {
            var request = new RegistrarUsuarioRequestDto
            {
                NomeUsuario = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                Senha = "SenhaForte!1234",
                Perfil = 2
            };

            var response = _client.PostAsJsonAsync("/api/usuarios/criar-usuario", request).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = response.Content.ReadAsStringAsync().Result;
            var usuarioResponse = JsonConvert.DeserializeObject<RegistrarUsuarioResponseDto>(content);

            usuarioResponse.Should().NotBeNull();
            usuarioResponse.NomeUsuario.Should().Be(request.NomeUsuario);
            usuarioResponse.Email.Should().Be(request.Email);
            usuarioResponse.Perfil.Should().Be("Usuário");
        }

        [Fact(DisplayName = "Deve retornar erro ao tentar cadastrar um email já cadastrado para outro usuário")]
        public void DeveRetornarErro_EmailUsuarioJaCadastrado()
        {
            var request = new RegistrarUsuarioRequestDto
            {
                NomeUsuario = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                Senha = "SenhaForte!1234",
                Perfil = 2
            };

            var response = _client.PostAsJsonAsync("/api/usuarios/criar-usuario", request).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var request2 = new RegistrarUsuarioRequestDto
            {
                NomeUsuario = _faker.Internet.UserName(),
                Email = request.Email, //Mesmo email do primeiro usuário
                Senha = "SenhaForte!1234",
                Perfil = 2
            };

            //Tentando cadastrar o mesmo email novamente
            var secondResponse = _client.PostAsJsonAsync("/api/usuarios/criar-usuario", request2).Result;

            secondResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = secondResponse.Content.ReadAsStringAsync().Result;
            var errorResponse = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(content);

            errorResponse.Should().NotBeNull();
            errorResponse.ErrorMessages.Should().Contain("Já existe um usuário cadastrado com este e-mail.");
        }

        [Fact(DisplayName = "Deve autenticar usuário no sistema com sucesso")]
        public void DeveAutenticarUsuarioNoSistemaComSucesso()
        {
            var request = new RegistrarUsuarioRequestDto
            {
                NomeUsuario = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                Senha = "SenhaForte!1234",
                Perfil = 2
            };

            var response = _client.PostAsJsonAsync("/api/usuarios/criar-usuario", request).Result;

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = response.Content.ReadAsStringAsync().Result;
            var usuarioResponse = JsonConvert.DeserializeObject<RegistrarUsuarioResponseDto>(content);

            var requestUserAuth = new AutenticarUsuarioRequestDto
            {
                Email = request.Email,
                Senha = request.Senha
            };

            var responseUserAuth = _client.PostAsJsonAsync("/api/usuarios/login", requestUserAuth).Result;

            responseUserAuth.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentUserAuth = responseUserAuth.Content.ReadAsStringAsync().Result;
            var usuarioAuthResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponseDto>(contentUserAuth);

            usuarioAuthResponse.Should().NotBeNull();
            usuarioAuthResponse.NomeUsuario.Should().Be(request.NomeUsuario);
            usuarioAuthResponse.Email.Should().Be(request.Email);
            usuarioAuthResponse.Perfil.Should().Be("Usuário");
            usuarioAuthResponse.DataHoraAcesso.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));
            usuarioAuthResponse.Token.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = "Deve retornar erro ao autenticar com senha incorreta")]
        public void DeveRetornarErro_AutenticacaoSenhaIncorreta()
        {
            var request = new RegistrarUsuarioRequestDto
            {
                NomeUsuario = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                Senha = "SenhaForte!1234",
                Perfil = 2
            };

            var createResponse = _client.PostAsJsonAsync("/api/usuarios/criar-usuario", request).Result;
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var requestWrongLogin = new AutenticarUsuarioRequestDto
            {
                Email = request.Email,
                Senha = "SenhaErrada!000"
            };

            var loginResponse = _client.PostAsJsonAsync("/api/usuarios/login", requestWrongLogin).Result;

            loginResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var content = loginResponse.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(content);

            error.Should().NotBeNull();
            error.ErrorMessages.Should().Contain("Usuário ou senha inválidos.");
        }

        [Fact(DisplayName = "Deve retornar erro ao autenticar usuário inexistente")]
        public void DeveRetornarErro_AutenticarUsuarioInexistente()
        {
            var loginRequest = new AutenticarUsuarioRequestDto
            {
                Email = _faker.Internet.Email(), // email não cadastrado
                Senha = "SenhaForte!1234"
            };

            var loginResponse = _client.PostAsJsonAsync("/api/usuarios/login", loginRequest).Result;

            loginResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var content = loginResponse.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(content);

            error.Should().NotBeNull();
            error.ErrorMessages.Should().Contain("Usuário ou senha inválidos.");
        }
    }
}
