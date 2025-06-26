using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludToTal_APIRest.Models
{

    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("apellido")]
        public string Apellido { get; set; }

        [Required]
        [Column("dni")]
        public int Dni { get; set; }

        [Required]
        [Column("fecha_nacimiento")]
        public DateOnly FechaDeNacimiento { get; set; }

        [Required]
        [Column("correo")]
        public string Correo { get; set; }

        [Column("nro_telefono")]
        public long? NroTelefono { get; set; }

        [Required]
        [Column("contrasenia")]
        public string Contrasenia { get; set; } // hash

        [Required]
        [Column("salt")]
        public string Salt { get; set; } // salt

        [Required]
        [Column("rol")]
        public string Rol { get; set; } // 'Paciente' o 'Profesional'

    }
}

