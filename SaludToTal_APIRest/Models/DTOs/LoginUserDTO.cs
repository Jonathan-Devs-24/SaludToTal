namespace SaludToTal_APIRest.Models.DTOs
{
    public class LoginUserDTO
    {
        public string Correo { get; set; }
        public string Contrasenia { get; set; } // Contraseña en texto plano, se hashea antes de verificar
    }
}
