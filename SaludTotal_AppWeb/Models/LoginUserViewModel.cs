using System.ComponentModel.DataAnnotations;

namespace SaludTotal_AppWeb.Models
{
    public class LoginUserViewModel
    {
        [Required, EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }
    }
}
