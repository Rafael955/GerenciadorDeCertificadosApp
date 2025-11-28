using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeCertificadosApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosDomainService _usuariosDomainService;

        public UsuariosController(IUsuariosDomainService usuariosDomainService)
        {
            _usuariosDomainService = usuariosDomainService;
        }

        [HttpPost("criar-usuario")]
        [ProducesResponseType(typeof(RegistrarUsuarioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateUser([FromBody] RegistrarUsuarioRequestDto request)
        {
            var result = _usuariosDomainService.RegistrarUsuario(request);
            return Created(string.Empty, result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AutenticarUsuarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] AutenticarUsuarioRequestDto request)
        {
            var result = _usuariosDomainService.AutenticarUsuario(request);
            return Ok(result);
        }

        [Authorize(Roles = "Administrador")]
        [ProducesResponseType(typeof(List<UsuarioResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        [HttpGet("listar-usuarios")]
        public IActionResult ListUsers()
        {
            var result = _usuariosDomainService.ListarUsuarios();
            return Ok(result);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorMessageResponseDto), StatusCodes.Status500InternalServerError)]
        [HttpDelete("excluir-usuario/{idUser}")]
        public IActionResult DeleteUser([FromRoute]Guid idUser)
        {
            _usuariosDomainService.ExcluirContaUsuario(idUser);
            return NoContent();
        }

    }
}