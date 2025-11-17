using GerenciadorDeCertificadosApp.Api.Configurations;
using GerenciadorDeCertificadosApp.Api.Exceptions;
using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

//Pegando a string de conexão do appsettings.json
builder.Services.AddDataContextConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddAuthConfiguration();

builder.Services.AddCorsConfiguration();

builder.Services.AddGlobalExceptionsConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerConfiguration();
}

app.UseCorsConfiguration();

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalExceptionsConfiguration(); //Deve ficar antes de app.MapControllers()

app.MapControllers();

app.Run();

public partial class Program { }