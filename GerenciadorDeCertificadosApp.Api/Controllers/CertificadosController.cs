using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeCertificadosApp.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadosController : ControllerBase
    {
        private readonly ICertificadosDomainService _certificadosDomainService;

        public CertificadosController(
            ICertificadosDomainService certificadosDomainService)
        {
            _certificadosDomainService = certificadosDomainService;
        }

        [HttpPost("criar-certificado")]
        [ProducesResponseType(typeof(CertificadoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] CertificadoRequestDto request)
        {
            try
            {
                var result = _certificadosDomainService.CriarCertificado(request);

                return Created(string.Empty, result);
            }
            catch (ValidationException ex)
            {
                List<string> errorMessages = ex.Errors.Select(e => e.ErrorMessage).ToList();

                return BadRequest(new ErrorMessageResponseDto(errorMessages));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new ErrorMessageResponseDto(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageResponseDto("Ocorreu um erro inesperado no servidor."));
            }
        }

        [HttpPut("atualizar-certificado/{id}")]
        [ProducesResponseType(typeof(CertificadoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CertificadoRequestDto request)
        {
            try
            {
                var result = _certificadosDomainService.AlterarDadosCertificado(id, request);

                return Ok(result);
            }
            catch (ValidationException ex)
            {
                List<string> errorMessages = ex.Errors.Select(e => e.ErrorMessage).ToList();

                return BadRequest(new ErrorMessageResponseDto(errorMessages));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new ErrorMessageResponseDto(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageResponseDto("Ocorreu um erro inesperado no servidor."));
            }
        }

        [HttpDelete("excluir-certificado/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                _certificadosDomainService.ExcluirCertificado(id);

                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new ErrorMessageResponseDto(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageResponseDto("Ocorreu um erro inesperado no servidor."));
            }
        }

        [HttpGet("obter-certificado/{id}")]
        [ProducesResponseType(typeof(CertificadoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var result = _certificadosDomainService.BuscarCertificadoPorId(id);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new ErrorMessageResponseDto(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageResponseDto("Ocorreu um erro inesperado no servidor."));
            }
        }

        [HttpGet("listar-certificados")]
        [ProducesResponseType(typeof(CertificadoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                var result = _certificadosDomainService.ListarCertificados();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageResponseDto("Ocorreu um erro inesperado no servidor."));
            }
        }
    }
}
