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
    public class AtividadesTest
    {
        private readonly HttpClient _client;
        private readonly Faker _faker;

        public AtividadesTest()
        {
            _client = new WebApplicationFactory<Program>().CreateClient();
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve cadastrar uma atividade com sucesso")]
        public void DeveCadastrarUmaAtividadeComSucesso()
        {
            //Arrange : Criando os dados da requisição

            var request = new AtividadeRequestDto
            {
                Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
            };

            //Act : Enviando a requisição para a API
            var result = _client.PostAsJsonAsync("/api/atividades/cadastrar-atividade", request).Result;

            //Assert : Verificando se a resposta da API é HTTP 201 (CREATED)
            result.StatusCode.Should().Be(HttpStatusCode.Created);

            //Assert : Verificando se os dados retornados no response batem com os do request
            var content = result.Content.ReadAsStringAsync().Result;

            var atividadeResponse = JsonConvert.DeserializeObject<AtividadeResponseDto>(content);

            atividadeResponse.Should().NotBeNull();
            atividadeResponse?.Id.Should().NotBeEmpty();
            atividadeResponse?.Nome.Should().Be(request.Nome);
        }

        [Fact(DisplayName = "Deve atualizar uma atividade com sucesso")]
        public void DeveAtualizarAtividadeComSucesso()
        {
            // Arrange: criar atividade
            var createRequest = new AtividadeRequestDto
            {
                Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
            };

            var createResponse = _client.PostAsJsonAsync("/api/atividades/cadastrar-atividade", createRequest).Result;
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdContent = createResponse.Content.ReadAsStringAsync().Result;
            var atividadeCriada = JsonConvert.DeserializeObject<AtividadeResponseDto>(createdContent);

            // Arrange: preparar update
            var updateRequest = new AtividadeRequestDto
            {
                Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()} (editada)"
            };

            // Act: atualizar
            var updateResponse = _client.PutAsJsonAsync($"/api/atividades/alterar-atividade/{atividadeCriada.Id}", updateRequest).Result;

            // Assert: HTTP 200 OK e dados atualizados
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedContent = updateResponse.Content.ReadAsStringAsync().Result;
            var atividadeAtualizada = JsonConvert.DeserializeObject<AtividadeResponseDto>(updatedContent);

            atividadeAtualizada.Should().NotBeNull();
            atividadeAtualizada?.Id.Should().Be(atividadeCriada.Id);
            atividadeAtualizada?.Nome.Should().Be(updateRequest.Nome);
        }

        [Fact(DisplayName = "Deve retornar erro ao alterar atividade não encontrada")]
        public void DeveRetornarErro_AlterarAtividadeNaoEncontrada()
        {
            // Arrange
            var updateRequest = new AtividadeRequestDto
            {
                Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
            };

            // Act
            var response = _client.PutAsJsonAsync($"/api/atividades/alterar-atividade/{Guid.NewGuid()}", updateRequest).Result;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = response.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(content);

            error.Should().NotBeNull();
            error.ErrorMessages.Should().NotBeEmpty();
            error.ErrorMessages[0].Should().Be("Atividade não encontrada.");
        }

        [Fact(DisplayName = "Deve excluir atividade com sucesso e não encontrá-la depois")]
        public void DeveExcluirAtividadeComSucesso()
        {
            // Arrange: criar atividade
            var createRequest = new AtividadeRequestDto
            {
                Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
            };

            var createResponse = _client.PostAsJsonAsync("/api/atividades/cadastrar-atividade", createRequest).Result;
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdContent = createResponse.Content.ReadAsStringAsync().Result;
            var atividadeCriada = JsonConvert.DeserializeObject<AtividadeResponseDto>(createdContent);

            // Act: excluir
            var deleteResponse = _client.DeleteAsync($"/api/atividades/excluir-atividade/{atividadeCriada.Id}").Result;

            // Assert: NoContent
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Act: tentar obter
            var getResponse = _client.GetAsync($"/api/atividades/obter-atividade/{atividadeCriada.Id}").Result;

            // Assert: BadRequest com mensagem de não encontrado
            getResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var getContent = getResponse.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(getContent);

            error.Should().NotBeNull();
            error.ErrorMessages[0].Should().Be("Atividade não encontrada.");
        }

        [Fact(DisplayName = "Não deve excluir atividade vinculada a certificado")]
        public void DeveRetornarErro_AoExcluirAtividadeVinculadaACertificado()
        {
            // Arrange: criar certificado com uma atividade específica
            var atividadeNome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}";

            var certRequest = new CertificadoRequestDto
            {
                Nome = _faker.Name.FullName(),
                UsuarioId = Guid.Parse("07146c5b-511f-4fdf-99e7-ccb72105922c"),
                Atividades = new List<AtividadeRequestDto>
                {
                    new AtividadeRequestDto { Nome = atividadeNome }
                }
            };

            var certResponse = _client.PostAsJsonAsync("/api/certificados/criar-certificado", certRequest).Result;
            certResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var certContent = certResponse.Content.ReadAsStringAsync().Result;
            var certificadoCriado = JsonConvert.DeserializeObject<CertificadoResponseDto>(certContent);

            var atividadeId = certificadoCriado.Atividades.First().Id;

            // Act: tentar excluir atividade vinculada
            var deleteResponse = _client.DeleteAsync($"/api/atividades/excluir-atividade/{atividadeId}").Result;

            // Assert: BadRequest com mensagem adequada
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var deleteContent = deleteResponse.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(deleteContent);

            error.Should().NotBeNull();
            error.ErrorMessages[0].Should().Be("Não é possível excluir esta atividade, pois ela está vinculada a um ou mais certificados.");
        }

        [Fact(DisplayName = "Deve retornar erro ao criar atividade com dados inválidos")]
        public void DeveRetornarErro_AoCriarAtividadeComDadosInvalidos()
        {
            // Arrange: nome muito curto
            var request = new AtividadeRequestDto
            {
                Nome = "A" // mínimo 2 caracteres segundo validação
            };

            // Act
            var response = _client.PostAsJsonAsync("/api/atividades/cadastrar-atividade", request).Result;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = response.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(content);

            error.Should().NotBeNull();
            error.ErrorMessages.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Deve retornar erro ao tentar cadastrar atividade com nome duplicado")]
        public void DeveRetornarErro_AoCadastrarAtividadeDuplicada()
        {
            // Arrange: criar atividade
            var request1 = new AtividadeRequestDto 
            { 
                Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
            };

            var response1 = _client.PostAsJsonAsync("/api/atividades/cadastrar-atividade", request1).Result;
            response1.StatusCode.Should().Be(HttpStatusCode.Created);

            // Act: tentar criar novamente com o mesmo nome
            var request2 = new AtividadeRequestDto 
            { 
                Nome = request1.Nome 
            };
            
            var response2 = _client.PostAsJsonAsync("/api/atividades/cadastrar-atividade", request2).Result;

            // Assert
            response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = response2.Content.ReadAsStringAsync().Result;
            var error = JsonConvert.DeserializeObject<ErrorMessageResponseDto>(content);

            error.Should().NotBeNull();
            error.ErrorMessages[0].Should().Be("Já existe uma atividade com este nome.");
        }

        [Fact(DisplayName = "Deve listar atividades existentes")]
        public void DeveListarAtividadesExistentes()
        {
            // Arrange: criar atividade
            var request = new AtividadeRequestDto
            {
                Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
            };

            var createResponse = _client.PostAsJsonAsync("/api/atividades/cadastrar-atividade", request).Result;
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdContent = createResponse.Content.ReadAsStringAsync().Result;
            var atividadeCriada = JsonConvert.DeserializeObject<AtividadeResponseDto>(createdContent);

            // Act: listar
            var listResponse = _client.GetAsync("/api/atividades/listar-atividades").Result;

            // Assert
            listResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var listContent = listResponse.Content.ReadAsStringAsync().Result;
            var listaAtividades = JsonConvert.DeserializeObject<List<AtividadeResponseDto>>(listContent);

            listaAtividades.Should().NotBeNull();
            listaAtividades.Any(a => a.Id == atividadeCriada.Id).Should().BeTrue();
        }

        [Fact(DisplayName = "Deve buscar atividade por id")]
        public void DeveBuscarAtividadePorId()
        {
            // Arrange: criar atividade
            var request = new AtividadeRequestDto
            {
                Nome = $"{_faker.Hacker.Adjective()} {_faker.Hacker.Noun()}"
            };

            var createResponse = _client.PostAsJsonAsync("/api/atividades/cadastrar-atividade", request).Result;
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdContent = createResponse.Content.ReadAsStringAsync().Result;
            var atividadeCriada = JsonConvert.DeserializeObject<AtividadeResponseDto>(createdContent);

            // Act: obter por id
            var getResponse = _client.GetAsync($"/api/atividades/obter-atividade/{atividadeCriada.Id}").Result;

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getContent = getResponse.Content.ReadAsStringAsync().Result;
            var atividade = JsonConvert.DeserializeObject<AtividadeResponseDto>(getContent);

            atividade.Should().NotBeNull();
            atividade.Id.Should().Be(atividadeCriada.Id);
            atividade.Nome.Should().Be(atividadeCriada.Nome);
        }
    }
}
