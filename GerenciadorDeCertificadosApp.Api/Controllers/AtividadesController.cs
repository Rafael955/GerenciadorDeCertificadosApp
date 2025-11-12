using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeCertificadosApp.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadesController : ControllerBase
    {
        private readonly IAtividadesDomainService _atividadesDomainService;

        public AtividadesController(IAtividadesDomainService atividadesDomainService)
        {
            _atividadesDomainService = atividadesDomainService;
        }

        [HttpPost("cadastrar-atividade")]
        [ProducesResponseType(typeof(AutenticarUsuarioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] AtividadeRequestDto request)
        {
            try
            {
                var result = _atividadesDomainService.CriarAtividade(request);

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

        [HttpPut("alterar-atividade/{id}")]
        [ProducesResponseType(typeof(AtividadeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] AtividadeRequestDto request)
        {
            try
            {
                var result = _atividadesDomainService.AlterarDadosAtividade(id, request);

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

        [HttpDelete("excluir-atividade/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                _atividadesDomainService.ExcluirAtividade(id);

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

        [HttpGet("obter-atividade/{id}")]
        [ProducesResponseType(typeof(AtividadeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var result = _atividadesDomainService.BuscarAtividadePorId(id);

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

        [HttpGet("listar-atividades")]
        [ProducesResponseType(typeof(AtividadeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                var result = _atividadesDomainService.ListarAtividades();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorMessageResponseDto("Ocorreu um erro inesperado no servidor."));
            }
        }

    }
}
