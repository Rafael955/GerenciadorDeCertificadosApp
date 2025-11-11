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

## Testes
- Executar testes unitários:
  dotnet test GerenciadorDeCertificadosApp.Test

## Boas práticas e pontos de atenção
- Validações estão implementadas via validators em `Domain/Validations`.
- Mapeamentos DTO <-> Entity via mappers em `Domain/Mappers`.
- Trate exceções de negócio via `ApplicationException` (padrão usado nos controllers).
- Use Swagger para inspeção de contratos e para obter exemplos de request/response.

## Como contribuir
- Siga o padrão existente: adicionar validators, mappers e services quando criar novas features.
- Crie testes unitários para regras de negócio no projeto `GerenciadorDeCertificadosApp.Test`.
- Abra PRs com descrição clara e testes que cubram o comportamento novo/alterado.

## Contato / Suporte
Abra issue no repositório com detalhes (passos para reproduzir, logs e payloads).

## Licença
Adicionar arquivo LICENSE conforme política do projeto.
