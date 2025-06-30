using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludToTal_APIRest.Models
{
    [Table("Administrador")]
    public class Administrador
    {
        [Key]
        [Column("id_admin")]
        public int IdAdministrador { get; set; }

        [Column("contrasenia")]
        public string Contrasenia { get; set; }  // Hash almacenado en Base64

        [Column("salt")]
        public string Salt { get; set; }         // Salt almacenado en Base64
    }
}
