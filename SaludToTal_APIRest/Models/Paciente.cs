using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludToTal_APIRest.Models
{
    [Table("Paciente")]
    public class Paciente
    {
        [Key]
        [Column("id_paciente")]
        public int IdPacinete { get; set; }

        [ForeignKey(nameof(usuario))] 
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("numero_afiliacion")]
        public string NumeroAfliado { get; set; }

        [Column("observaciones")]
        public string Observacion { get; set; }

        public Usuario usuario { get; set; }
    }
}
