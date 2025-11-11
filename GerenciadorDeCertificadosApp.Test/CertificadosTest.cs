using Bogus;
using FluentAssertions;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace GerenciadorDeCertificadosApp.Tests
{
    public class CertificadosTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public CertificadosTest()
        {
            _client = new WebApplicationFactory<Program>().CreateClient();
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve criar um novo certificado com sucesso.")]
        public void DeveCriarNovoCertificadoComSucesso()
        {
            //Arrange : Criando os dados da requisição
            var request = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c") //Id do Usuário padrão Admin
            };

            for (int i = 0; i < 3; i++)
            {
                request.Atividades.Add(new AtividadeRequestDto
                {
                    Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
                });
            }

            //Act : Enviando os dados para o endpoint de cadastro de certificados da API
            var response = _client.PostAsJsonAsync("/api/certificados/criar-certificado", request).Result;

            //Assert : Verificar se a API retornou HTTP 201 (CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Deve alterar um certificado com sucesso.")]
        public void DeveAlterarCertificadoComSucesso()
        {
            //Arrange : Criando os dados da requisição
            var request = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c") //Id do Usuário padrão Admin
            };

            for (int i = 0; i < 3; i++)
            {
                request.Atividades.Add(new AtividadeRequestDto
                {
                    Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
                });
            }

            //Act : Enviando os dados para o endpoint de cadastro de certificados da API
            var response = _client.PostAsJsonAsync("/api/certificados/criar-certificado", request).Result;

            //Assert : Verificar se a API retornou HTTP 201 (CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //Arrange : Criando os dados da requisição para atualizar
            var content = response?.Content.ReadAsStringAsync()?.Result;

            CertificadoResponseDto certificadoResponse = JsonConvert.DeserializeObject<CertificadoResponseDto>(content);

            var requestUpdate = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c") //Id do Usuário padrão Admin
            };

            for (int i = 0; i < 3; i++)
            {
                requestUpdate.Atividades.Add(new AtividadeRequestDto
                {
                    Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
                });
            }

            var responseUpdate = _client.PutAsJsonAsync($"/api/certificados/atualizar-certificado/{certificadoResponse.Id}", requestUpdate).Result;

            //Assert : Verificar se a API retornou HTTP 200 (OK)
            responseUpdate?.StatusCode.Should().Be(HttpStatusCode.OK);

            //Assert : Verificar se dados retornados no response batem com os do request
            var contentUpdate = responseUpdate?.Content.ReadAsStringAsync()?.Result;

            CertificadoResponseDto certificadoResponseUpdated = JsonConvert.DeserializeObject<CertificadoResponseDto>(contentUpdate);

            certificadoResponseUpdated?.Id.Should().NotBeEmpty();
            certificadoResponseUpdated?.Nome.Should().Be(requestUpdate.Nome);
            certificadoResponseUpdated?.Atividades.Should().NotBeEmpty();
            certificadoResponseUpdated?.UsuarioQueGerou.Should().Be("Admin");
        }

        [Fact(DisplayName = "Deve retornar erro informando que o certificado não foi encontrado.")]
        public void DeveRetornarErro_CertificadoNaoEncontrado()
        {
            //Arrange : Criando os dados da requisição
            var request = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c") //Id do Usuário padrão Admin
            };

            for (int i = 0; i < 3; i++)
            {
                request.Atividades.Add(new AtividadeRequestDto
                {
                    Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
                });
            }

            //Act : Enviando os dados para o endpoint de cadastro de certificados da API
            var response = _client.PostAsJsonAsync("/api/certificados/criar-certificado", request).Result;

            //Assert : Verificar se a API retornou HTTP 201 (CREATED)
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            //Arrange : Criando os dados da requisição para atualizar
            var content = response?.Content.ReadAsStringAsync()?.Result;

            CertificadoResponseDto certificadoResponse = JsonConvert.DeserializeObject<CertificadoResponseDto>(content);

            var requestUpdate = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c") //Id do Usuário padrão Admin
            };

            for (int i = 0; i < 3; i++)
            {
                requestUpdate.Atividades.Add(new AtividadeRequestDto
                {
                    Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
                });
            }

            //Act : Enviando os dados para o endpoint atualizar certificados da API com um Id aleatório de certificado não cadastrado
            var responseUpdate = _client.PutAsJsonAsync($"/api/certificados/atualizar-certificado/{Guid.NewGuid()}", requestUpdate).Result;

            //Assert : Verificar se a API retornou HTTP 400 (BAD REQUEST)
            responseUpdate?.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //Assert : Verificar se dados retornados no response batem com os do request
            var contentUpdate = responseUpdate?.Content.ReadAsStringAsync()?.Result;

            ErrorMessageResponseDto errorMessageResponse = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(contentUpdate);

            errorMessageResponse?.ErrorMessages[0].Should().Be("Certificado não encontrado.");
        }

        [Fact(DisplayName = "Deve listar certificados existentes.")]
        public void DeveListarCertificadosExistentes()
        {
            // Arrange: criar um certificado
            var request = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c")
            };

            for (int i = 0; i < 3; i++)
            {
                request.Atividades.Add(new AtividadeRequestDto 
                { 
                    Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}" 
                });
            }

            //Act : Enviando os dados para o endpoint de cadastro de certificados da API
            var createResponse = _client.PostAsJsonAsync("/api/certificados/criar-certificado", request).Result;

            //Assert : Verificar se a API retornou HTTP 201 (CREATED)
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdContent = createResponse.Content.ReadAsStringAsync().Result;

            var created = JsonConvert.DeserializeObject<CertificadoResponseDto>(createdContent);

            // Act: listar certificados
            var listResponse = _client.GetAsync("/api/certificados/listar-certificados").Result;

            // Assert
            listResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var listContent = listResponse.Content.ReadAsStringAsync().Result;
            var certificados = JsonConvert.DeserializeObject<List<CertificadoResponseDto>>(listContent);

            certificados.Should().NotBeNull();
            certificados.Any(c => c.Id == created.Id).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve buscar certificado por id com atividades corretas.")]
        public void DeveBuscarCertificadoPorId()
        {
            // Arrange: criar um certificado
            var request = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c")
            };

            for (int i = 0; i < 3; i++)
            {
                request.Atividades.Add(new AtividadeRequestDto 
                { 
                    Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
                });
            }

            //Act : Enviando os dados para o endpoint de cadastro de certificados da API
            var createResponse = _client.PostAsJsonAsync("/api/certificados/criar-certificado", request).Result;

            //Assert : Verificar se a API retornou HTTP 201 (CREATED)
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdContent = createResponse.Content.ReadAsStringAsync().Result;

            var created = JsonConvert.DeserializeObject<CertificadoResponseDto>(createdContent);

            // Act: buscar por id
            var getResponse = _client.GetAsync($"/api/certificados/obter-certificado/{created.Id}").Result;

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getContent = getResponse.Content.ReadAsStringAsync().Result;
            var certificado = JsonConvert.DeserializeObject<CertificadoResponseDto>(getContent);

            certificado.Should().NotBeNull();
            certificado.Id.Should().Be(created.Id);
            certificado.Atividades.Should().NotBeEmpty();
            certificado.UsuarioQueGerou.Should().Be("Admin");
        }

        [Fact(DisplayName = "Deve excluir certificado e não ser possível buscar depois.")]
        public void DeveExcluirCertificadoComSucesso()
        {
            // Arrange: criar um certificado
            var request = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c")
            };

            for (int i = 0; i < 3; i++)
            {
                request.Atividades.Add(new AtividadeRequestDto 
                { 
                    Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
                });
            }

            var createResponse = _client.PostAsJsonAsync("/api/certificados/criar-certificado", request).Result;

            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdContent = createResponse.Content.ReadAsStringAsync().Result;
            var created = JsonConvert.DeserializeObject<CertificadoResponseDto>(createdContent);

            // Act: excluir
            var deleteResponse = _client.DeleteAsync($"/api/certificados/excluir-certificado/{created.Id}").Result;

            //Assert : Verificar se a API retornou HTTP 204 (NO CONTENT)
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Act: tentar buscar o certificado excluído
            var getResponse = _client.GetAsync($"/api/certificados/obter-certificado/{created.Id}").Result;

            // Assert:  Verificar se a API retornou HTTP 400 (BAD REQUEST)
            getResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var getContent = getResponse.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(getContent);

            error.ErrorMessages[0].Should().Be("Certificado não encontrado.");
        }

        [Fact(DisplayName = "Deve retornar erro ao criar certificado com dados inválidos.")]
        public void DeveRetornarErro_AoCriarCertificadoComDadosInvalidos()
        {
            // Arrange: request inválido (nome muito curto)
            var request = new CertificadoRequestDto
            {
                Nome = "A", // inválido segundo validação (minino 3 caracteres)
                Atividades = new List<AtividadeRequestDto>(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c")
            };

            // Act
            var response = _client.PostAsJsonAsync("/api/certificados/criar-certificado", request).Result;

            // Assert:  Verificar se a API retornou HTTP 400 (BAD REQUEST)
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = response.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(content);

            error.Should().NotBeNull();
            error.ErrorMessages.Should().NotBeEmpty();
        }
    }
}
