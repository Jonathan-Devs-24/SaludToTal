using System.ComponentModel.DataAnnotations;

namespace SaludToTal_APIRest.Models.DTOs
{
    public class AdminLoginDTO
    {
        [Required]
        public string Clave { get; set; }
    }
}
