namespace SaludTotal_AppWeb.Models
{
    public class ProfesionalViewModel
    {
        public int IdProfesional { get; set; }
        public int IdUsuario { get; set; }
        public string NroMatricula { get; set; }
        public string HorarioAtencion { get; set; }
        public UsuarioViewModel Usuario { get; set; }
    }
}
