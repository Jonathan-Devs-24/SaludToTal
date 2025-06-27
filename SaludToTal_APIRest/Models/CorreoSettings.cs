namespace SaludToTal_APIRest.Models
{
    public class CorreoSettings
    {
        public string ServidorSMTP { get; set; }
        public int Puerto { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Remitente { get; set; }
    }
}
