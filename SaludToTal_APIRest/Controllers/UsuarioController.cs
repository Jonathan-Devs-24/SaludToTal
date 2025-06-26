using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotal_APIRest.Data;
using SaludToTal_APIRest.DTOs;
using SaludToTal_APIRest.Models;
using SaludToTal_APIRest.Models.DTOs;
using SaludToTal_APIRest.Services;

namespace SaludToTal_APIRest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
    public UsuarioController (ApplicationDbContext context)
    {
        _context = context;
    }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == login.Correo);

            if (usuario == null)
                return Unauthorized("Correo o contraseña inválidos.");

            bool passwordValida = AuthService.VerificarContrasenia(
                login.Contrasenia,
                usuario.Salt,
                usuario.Contrasenia
            );

            if (!passwordValida)
                return Unauthorized("Correo o contraseña inválidos.");

            return Ok(new
            {
                usuario.IdUsuario,
                usuario.Nombre,
                usuario.Apellido,
                usuario.Rol
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo))
                return BadRequest("El correo ya está registrado.");

            string salt = AuthService.GenerarSalt();
            string hash = AuthService.HashPassword(dto.Contrasenia, salt);

            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Dni = dto.Dni,
                FechaDeNacimiento = dto.FechaDeNacimiento,
                Correo = dto.Correo,
                NroTelefono = dto.NroTelefono,
                Contrasenia = hash,
                Salt = salt,
                Rol = dto.Rol
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado correctamente.");
        }


    }
}
