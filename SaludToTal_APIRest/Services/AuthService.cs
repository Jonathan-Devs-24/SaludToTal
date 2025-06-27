using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotal_APIRest.Data;
using SaludToTal_APIRest.Models;
using SaludToTal_APIRest.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace SaludToTal_APIRest.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly CorreoService _correoService;

        public AuthService(ApplicationDbContext context, CorreoService correoService)
        {
            _context = context;
            _correoService = correoService;
        }

        // Genera un salt aleatorio para el hash de la contraseña
        public static string GenerarSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(saltBytes); // Llena el array con valores aleatorios
            }
            return Convert.ToBase64String(saltBytes);
        }


        // Genera un hash seguro de la contraseña usando PBKDF2 con SHA256
        public static string HashPassword(string password, string salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256))
            {
                return Convert.ToBase64String(pbkdf2.GetBytes(32)); // Genera un hash seguro
            }
        }


        // Verifica si la contraseña ingresada coincide con el hash guardado
        public static bool VerificarContrasenia(string passIngresada, string salt, string hashGuardado)
        {
            var hashIngresado = HashPassword(passIngresada, salt);
            return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(hashIngresado), Convert.FromBase64String(hashGuardado));
        }


        // Genera un token de recuperación único
        public async Task<bool> GenerarTokenRecuperacionAsync(string correo)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null)
                return false;

            var token = Guid.NewGuid().ToString("N");
            var expiracion = DateTime.UtcNow.AddMinutes(30);

            var recuperar = new RecuperarContra
            {
                IdUsuario = usuario.IdUsuario,
                Token = token,
                Expiracion = expiracion
            };

            _context.RecuperarContra.Add(recuperar);
            await _context.SaveChangesAsync();

            await _correoService.EnviarRecuperacionCorreo(usuario.Correo, token);
            return true;
        }

        // Valida si el token de recuperación es válido y no ha expirado
        public async Task<bool> ValidarTokenRecuperacionAsync(string token)
        {
            return await _context.RecuperarContra
                .AnyAsync(t => t.Token == token && t.Expiracion > DateTime.UtcNow && !t.Usado);
        }


        public async Task<bool> RestablecerContraseniaAsync(ReestablecerDTO dto)
        {
            var tokenEntry = await _context.RecuperarContra
                .Include(r => r.usuario)
                .FirstOrDefaultAsync(t => t.Token == dto.Token && t.Expiracion > DateTime.UtcNow && !t.Usado);

            if (tokenEntry == null)
                return false;

            var nuevoSalt = GenerarSalt();
            var nuevoHash = HashPassword(dto.NuevaContrasenia, nuevoSalt);

            tokenEntry.usuario.Contrasenia = nuevoHash;
            tokenEntry.usuario.Salt = nuevoSalt;
            tokenEntry.Usado = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
