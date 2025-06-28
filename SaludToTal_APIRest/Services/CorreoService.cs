using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SaludToTal_APIRest.Models;
using System.Net.Mail;
using System.Threading.Tasks;

public class CorreoService
{
    private readonly CorreoSettings _settings;

    public CorreoService(IOptions<CorreoSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task EnviarRecuperacionCorreo(string destinatario, string token)
    {
        var mensaje = new MimeMessage();
        mensaje.From.Add(MailboxAddress.Parse(_settings.Remitente));
        mensaje.To.Add(MailboxAddress.Parse(destinatario));
        mensaje.Subject = "Recuperación de contraseña";

        // Este enlace debe apuntar a tu frontend donde se maneja la recuperación de contraseña
        string link = $"https://frontNOcreadoTODAVIA.com/reestablecer?token={token}";

        mensaje.Body = new TextPart("html")
        {
            Text = $@"
                <h2>Recuperación de contraseña</h2>
                <p>Recibimos una solicitud para reestablecer tu contraseña.</p>
                <p>Hacé clic en el siguiente enlace para continuar:</p>
                <p><a href='{link}'>Recuperar contraseña</a></p>
                <p><small>Este enlace expirará en 30 minutos.</small></p>"
        };


        // Enviar el mensaje usando MailKit (EN PROCESO)

        var smtp = new MailKit.Net.Smtp.SmtpClient();
        await smtp.ConnectAsync(_settings.ServidorSMTP, _settings.Puerto, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Usuario, _settings.Clave);
        await smtp.SendAsync(mensaje);
        await smtp.DisconnectAsync(true);
    }
}
