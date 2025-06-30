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

        [HttpGet("paginados")]
        public async Task<IActionResult> GetPaginados(int page = 1, int pageSize = 10)
        {
            try
            {
                var totalItems = await _context.Usuarios.CountAsync();
                var items = await _context.Usuarios
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var resultado = new PaginatedResponse<Usuario>
                {
                    TotalItems = totalItems,
                    Page = page,
                    PageSize = pageSize,
                    Data = items
                };

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("todos")]
        public async Task<IActionResult> GetTodos()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
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
            try
            {
                if (await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo))
                    return BadRequest("El correo ya está registrado.");

                if (await _context.Usuarios.AnyAsync(u => u.Dni == dto.Dni))
                    return BadRequest("El DNI ya está registrado.");

                string salt = AuthService.GenerarSalt();
                string hash = AuthService.HashPassword(dto.Contrasenia, salt);

                var nuevoUsuario = new Usuario
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Dni = dto.Dni,
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
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

    }
}
