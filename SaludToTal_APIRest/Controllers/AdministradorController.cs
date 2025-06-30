using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotal_APIRest.Data;
using SaludToTal_APIRest.Models;
using SaludToTal_APIRest.Models.DTOs;
using SaludToTal_APIRest.Services;

namespace SaludToTal_APIRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/admin/validatekey
        [HttpPost("validatekey")]
        public async Task<IActionResult> LoginAdmin([FromBody] AdminLoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var admin = await _context.Administradores.FirstOrDefaultAsync();

            if (admin == null)
                return Unauthorized("Administrador no encontrado.");

            bool claveValida = AuthService.VerificarContrasenia(dto.Clave, admin.Salt, admin.Contrasenia);

            if (!claveValida)
                return Unauthorized("Clave incorrecta.");

            return Ok(new
            {
                mensaje = "Inicio de sesión como administrador exitoso."
            });
        }
    }
}