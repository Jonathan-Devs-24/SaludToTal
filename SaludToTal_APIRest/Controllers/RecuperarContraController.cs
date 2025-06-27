using Microsoft.AspNetCore.Mvc;
using SaludTotal_APIRest.Data;
using SaludToTal_APIRest.Models.DTOs;
using SaludToTal_APIRest.Services;

namespace SaludToTal_APIRest.Controllers
{
    public class RecuperarContraController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;
        public RecuperarContraController(ApplicationDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("solicitar")]
        public async Task<IActionResult> SolicitarRecuperacion([FromBody] string correo)
        {
            var exito = await _authService.GenerarTokenRecuperacionAsync(correo);
            return exito ? Ok("Correo enviado") : NotFound("Usuario no encontrado");
        }

        [HttpGet("validar")]
        public async Task<IActionResult> ValidarToken([FromQuery] string token)
        {
            var esValido = await _authService.ValidarTokenRecuperacionAsync(token);
            return Ok(new { valido = esValido });
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ReestablecerContrasenia([FromBody] ReestablecerDTO dto)
        {
            var actualizado = await _authService.RestablecerContraseniaAsync(dto);
            return actualizado ? Ok("Contraseña actualizada") : BadRequest("Token inválido o expirado");
        }



    }
}
