namespace GerenciadorDeCertificadosApp.Tests
{
    // Compartilhe um único banco de dados para todas as classes que usam esta coleção
    [CollectionDefinition("Database Collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // Esta classe não precisa de código, ela é apenas um ponto de ancoragem para:
        // 1. Desabilitar o paralelismo para as classes que a usam.
        // 2. Definir o tipo de fixture (CustomWebApplicationFactory) que será usada pela coleção.
    }
}