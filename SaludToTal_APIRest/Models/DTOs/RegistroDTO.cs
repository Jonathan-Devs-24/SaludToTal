using System.ComponentModel.DataAnnotations;

namespace SaludToTal_APIRest.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public int Dni { get; set; }

        [Required]
        public DateOnly FechaDeNacimiento { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        public long? NroTelefono { get; set; }

        [Required]
        public string Contrasenia { get; set; }

        [Required]
        public string Rol { get; set; } // 'Paciente' o 'Profesional'
    }
}
