# GerenciadorDeCertificadosApp

Web API elaborada em C# com ASP.NET Core para gerenciamento de certificados e atividades de um aluno e para cadastro e autenticação de usuarios de um sistema. Projeto dividido em camadas: `Api`, `Domain`, `Infra.Data` e `Test`.

## Visão geral
- Linguagem: C# 13
- Target framework: .NET 9
- Arquitetura: API REST com separação de responsabilidades (Domain services, Repositories, Mappers, Validators)
- Persistência: EF Core (migrations já presentes)
- Autenticação: JWT (configurada em `Api/Configurations/AuthConfiguration.cs`)
- Documentação de API: Swagger (configuração em `Api/Configurations/SwaggerConfiguration.cs`)

## Estrutura do repositório
- `GerenciadorDeCertificadosApp.Api` — API Web (controllers, configurações, appsettings)
- `GerenciadorDeCertificadosApp.Domain` — DTOs, entidades, serviços, validações e mappers
- `GerenciadorDeCertificadosApp.Infra.Data` — Contexto EF, mappings, repositórios, migrations
- `GerenciadorDeCertificadosApp.Test` — testes automatizados

## Endpoints principais
(Use o Swagger em execução para a lista completa e exemplos)
- Certificados (conforme `CertificadosController`)
  - POST `/api/certificados/cadastrar-certificado` — criar certificado
  - PUT `/api/certificados/alterar-certificado/{id}` — alterar certificado
  - DELETE `/api/certificados/{id}` — excluir certificado
  - GET `/api/certificados/{id}` — buscar por id
  - GET `/api/certificados` — listar todos
- Atividades (conforme `AtividadesController`)
  - POST `/api/atividades/cadastrar-atividade` — criar atividade
  - PUT `/api/atividades/alterar-atividade/{id}` — alterar atividade
  - DELETE `/api/atividades/{id}` — excluir atividade
  - GET `/api/atividades/{id}` — buscar por id
  - GET `/api/atividades` — listar todas
- Usuários (conforme `UsuariosController`))
  - POST `/api/usuarios/cadastrar-usuario` — criar usuário
  - POST `/api/usuarios/login` — autenticar e obter token JWT
  - GET `/api/usuarios/listar-usuarios` — listagem de usuários (acesso restrito ao administrador)
  - DELETE `/api/usuarios/excluir-usuario/{id}` — exclusão de usuário/conta

Exemplo rápido (Atividades):
- Criar:
  curl -X POST "https://localhost:5001/api/atividades/cadastrar-atividade" -H "Content-Type: application/json" -d '{"nome":"Nome da atividade"}'
- Listar:
  curl "https://localhost:5001/api/atividades"

Observação: rotas protegidas por autenticação JWT exigem header `Authorization: Bearer {token}`.

## Pré-requisitos
- .NET 9 SDK
- Visual Studio 2022 (ou IDE compatível)
- SQL Server (ou outro provider configurado no `appsettings.json`)
- Ferramentas EF Core para CLI (opcional para migrations): `dotnet tool install --global dotnet-ef`

## Como executar (CLI)
1. Restaurar dependências:
   dotnet restore
2. Atualizar banco (a partir do projeto `GerenciadorDeCertificadosApp.Infra.Data`):
   dotnet ef database update --project GerenciadorDeCertificadosApp.Infra.Data --startup-project GerenciadorDeCertificadosApp.Api
   (migrations já presentes: `InitialMigration`, `AddAdminUser`)
3. Executar API:
   dotnet run --project GerenciadorDeCertificadosApp.Api
4. Abrir Swagger: `https://localhost:{port}/swagger`

## Como executar (Visual Studio 2022)
1. Abra a solução no Visual Studio 2022.
2. Defina `GerenciadorDeCertificadosApp.Api` como projeto de inicialização via __Set Startup Projects__.
3. Build: __Build > Build Solution__.
4. Executar: __Debug > Start Debugging (F5)__ ou __Run > Start Without Debugging (Ctrl+F5)__.
5. Acesse Swagger em `/swagger`.

## Configurações importantes
- `appsettings.json` / `appsettings.Development.json`: ajuste `ConnectionStrings` e configurações de JWT (se aplicável).
- Para trocar a chave JWT, edite a configuração correspondente em `Api/Configurations/AuthConfiguration.cs` ou nas chaves do `appsettings`.

## Banco de dados e Migrations
- Migrations já incluídas em `Infra.Data/Migrations`.
- Comandos úteis:
  - Adicionar migration:
    dotnet ef migrations add NomeDaMigration --project GerenciadorDeCertificadosApp.Infra.Data --startup-project GerenciadorDeCertificadosApp.Api
  - Atualizar banco:
    dotnet ef database update --project GerenciadorDeCertificadosApp.Infra.Data --startup-project GerenciadorDeCertificadosApp.Api

## Banco de dados com Docker (SQL Server)

Se preferir não instalar o SQL Server localmente, é possível rodar uma instância do SQL Server em Docker. Abaixo há exemplos com `docker run` e `docker-compose`, além de orientações para configuração da `ConnectionString` e aplicação das migrations.

Opção 1 — executar com `docker run`:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SuaSenhaForte123!" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server
```

Após alguns instantes, o SQL Server estará acessível em `localhost:1433`. Ajuste a `ConnectionString` em `appsettings.json` para utilizar o SQL Server Docker.

Opção 2 — executar com `docker-compose`:

```yaml
version: '3.8'
services:
  db:
    image: mcr.microsoft.com/mssql/server
    environment:
      SA_PASSWORD: "SuaSenhaForte123!"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
```

Basta criar um arquivo `docker-compose.yml` com o conteúdo acima e executar `docker-compose up -d`. O SQL Server ficará disponível em `localhost:1433`.

### Configuração da ConnectionString

Exemplo de `ConnectionString` para SQL Server (ajuste a senha):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=SeuBancoDeDados;User=sa;Password=SuaSenhaForte123!;"
}
```

### Aplicação de Migrations

Com o SQL Server em execução, aplique as migrations usando o comando:

```bash
dotnet ef database update --project GerenciadorDeCertificadosApp.Infra.Data --startup-project GerenciadorDeCertificadosApp.Api
```

## Testes
- Executar testes unitários:
  dotnet test GerenciadorDeCertificadosApp.Test