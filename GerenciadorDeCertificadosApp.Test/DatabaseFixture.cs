using GerenciadorDeCertificadosApp.Tests.Factories;

namespace GerenciadorDeCertificadosApp.Tests
{
    // DatabaseFixture.cs
    public class DatabaseFixture : IAsyncLifetime
    {
        // 1. A Factory agora é criada AQUI
        public readonly CustomWebApplicationFactory<Program> Factory = new();

        // 2. O construtor é agora **sem parâmetros**
        public DatabaseFixture()
        {
            // Não precisa de código, a Factory é instanciada acima
        }

        // Chamado ANTES de todos os testes da coleção.
        public Task InitializeAsync() => Task.CompletedTask;

        // Chamado DEPOIS de todos os testes da coleção (o último a ser executado).
        public async Task DisposeAsync()
        {
            // Garante que o banco de dados seja apagado SOMENTE APÓS O ÚLTIMO TESTE.
            await Factory.ResetDatabaseAsync();
        }
    }
}