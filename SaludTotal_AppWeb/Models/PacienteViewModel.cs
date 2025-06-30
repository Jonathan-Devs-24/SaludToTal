namespace SaludTotal_AppWeb.Models
{
    public class PacienteViewModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public string Correo { get; set; }
        public long? NroTelefono { get; set; }
        public string NumeroAfiliado { get; set; }
    }
}
