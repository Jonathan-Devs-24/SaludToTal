using System.Security.Cryptography;
using System.Text;

namespace SaludToTal_APIRest.Services
{
    public class AuthService
    {
        public static string GenerarSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(saltBytes); // Llena el array con valores aleatorios
            }
            return Convert.ToBase64String(saltBytes);


        }

        public static string HashPassword(string password, string salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256))
            {
                return Convert.ToBase64String(pbkdf2.GetBytes(32)); // Genera un hash seguro
            }


        }

        public static bool VerificarContrasenia(string passIngresada, string salt, string hashGuardado)
        {
            var hashIngresado = HashPassword(passIngresada, salt);
            return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(hashIngresado), Convert.FromBase64String(hashGuardado));


        }

    }
}
