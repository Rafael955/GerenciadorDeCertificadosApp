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
            var result = _atividadesDomainService.CriarAtividade(request);
            return Created(string.Empty, result);
        }

        [HttpPut("alterar-atividade/{id}")]
        [ProducesResponseType(typeof(AtividadeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] AtividadeRequestDto request)
        {
            var result = _atividadesDomainService.AlterarDadosAtividade(id, request);
            return Ok(result);
        }

        [HttpDelete("excluir-atividade/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _atividadesDomainService.ExcluirAtividade(id);
            return NoContent();
        }

        [HttpGet("obter-atividade/{id}")]
        [ProducesResponseType(typeof(AtividadeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(Guid id)
        {
            var result = _atividadesDomainService.BuscarAtividadePorId(id);
            return Ok(result);
        }

        [HttpGet("listar-atividades")]
        [ProducesResponseType(typeof(AtividadeResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            var result = _atividadesDomainService.ListarAtividades();
            return Ok(result);
        }

    }
}
