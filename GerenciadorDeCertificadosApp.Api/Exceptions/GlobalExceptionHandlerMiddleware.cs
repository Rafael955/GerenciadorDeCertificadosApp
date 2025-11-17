using System.Net;
using System.Text.Json;
using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;

namespace GerenciadorDeCertificadosApp.Api.Exceptions
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var (status, responseDto) = BuildErrorResponse(ex);

            context.Response.StatusCode = (int)status;

            var json = JsonSerializer.Serialize(
                responseDto,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

            await context.Response.WriteAsync(json);
        }

        private static (HttpStatusCode status, ErrorMessageResponseDto response)
            BuildErrorResponse(Exception ex)
        {
            return ex switch
            {
                // Erros do FluentValidation → 400
                ValidationException validationException =>
                    (
                        HttpStatusCode.BadRequest,
                        new ErrorMessageResponseDto(
                            validationException.Errors.Select(e => e.ErrorMessage).ToList()
                        )
                    ),

                // Exceções de domínio → 400
                ApplicationException applicationException =>
                    (
                        HttpStatusCode.BadRequest,
                        new ErrorMessageResponseDto(applicationException.Message)
                    ),

                // Erros genéricos → 500
                _ =>
                    (
                        HttpStatusCode.InternalServerError,
                        new ErrorMessageResponseDto("Ocorreu um erro inesperado no servidor.")
                    )
            };
        }
    }
}
