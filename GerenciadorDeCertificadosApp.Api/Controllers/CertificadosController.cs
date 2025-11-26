using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeCertificadosApp.Api.Controllers
{
    [Authorize]
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
            var result = _certificadosDomainService.CriarCertificado(request);
            return Created(string.Empty, result);
        }

        [HttpPut("atualizar-certificado/{id}")]
        [ProducesResponseType(typeof(CertificadoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CertificadoRequestDto request)
        {
            var result = _certificadosDomainService.AlterarDadosCertificado(id, request);
            return Ok(result);
        }

        [HttpDelete("excluir-certificado/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _certificadosDomainService.ExcluirCertificado(id);
            return NoContent();
        }

        [HttpGet("obter-certificado/{id}")]
        [ProducesResponseType(typeof(CertificadoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _certificadosDomainService.BuscarCertificadoPorId(id);
            return Ok(result);
        }

        [HttpGet("listar-certificados/{userId?}")]
        [ProducesResponseType(typeof(List<CertificadoResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll([FromRoute] Guid? userId)
        {
            var result = _certificadosDomainService.ListarCertificados(userId);
            return Ok(result);
        }
    }
}
