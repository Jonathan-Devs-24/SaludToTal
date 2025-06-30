using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludToTal_APIRest.Models
{
    [Table("Profesional")]
    public class Profesional
    {
        [Key]
        [Column("id_profesional")]
        public int IdProfesional { get; set; }

        [ForeignKey(nameof(usuario))]
        [Column("id_usuario")]
        public int idUsuario { get; set; }

        [Column("nro_matricula")]
        public string NroMatricula { get; set; }

        [Column("horario_atencion")]
        public string HorarioAtencion { get; set; }


        public Usuario usuario { get; set; }
    }

}
