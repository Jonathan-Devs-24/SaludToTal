namespace SaludTotal_AppWeb.Models
{
    public class UsuarioViewModel
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int dni { get; set; }
        public string correo { get; set; }
        public long? nroTelefono { get; set; }
        public string rol { get; set; }
    }
}
