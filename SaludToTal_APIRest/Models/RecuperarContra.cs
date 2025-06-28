using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludToTal_APIRest.Models
{

    [Table ("RecuperarContra")]
    public class RecuperarContra
    {
        [Key]
        [Column ("id_recuperarContra")]
        public int IdRecuperarContra { get; set; }

        [Required]
        [ForeignKey ("usuario")]
        [Column ("id_usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column ("token")]
        public string Token { get; set; }

        [Required]
        [Column ("expiracion")]
        public DateTime Expiracion { get; set; }
       
        [Column ("usado")]
        public bool Usado { get; set; } = false;

        [Column("momento_creado")]
        public DateTime MomentoCreado { get; set; } = DateTime.UtcNow;

        public Usuario usuario { get; set; }
    }
}
