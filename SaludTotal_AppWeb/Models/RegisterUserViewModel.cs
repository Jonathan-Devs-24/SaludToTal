using System.ComponentModel.DataAnnotations;

namespace SaludTotal_AppWeb.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public int Dni { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        public long? NroTelefono { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }

        [Required]
        public string Rol { get; set; } // Paciente o Profesional
    }
}
