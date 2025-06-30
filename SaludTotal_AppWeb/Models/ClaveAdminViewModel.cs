using System.ComponentModel.DataAnnotations;

namespace SaludTotal_AppWeb.Models
{
    public class ClaveAdminViewModel
    {
        [Required(ErrorMessage = "El campo Clave es obligatorio.")]
        public string Clave { get; set; }
    }
}
