using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Cryptography;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace RegistroConvenioUCNE.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task EnviarCorreoConAdjuntoAsync(
        string destinatario,
        string asunto,
        string cuerpo,
        byte[]? contenidoAdjunto = null, // Aceptamos bytes directamente
        string? nombreAdjunto = null,    // Nombre del archivo (ej. convenio.pdf)
        Stream? certificadoStream = null,
        string? passwordCertificado = null)
    {
        var emailMessage = new MimeMessage();

        var senderName = _config["EmailSettings:SenderName"];
        var senderEmail = _config["EmailSettings:SenderEmail"];

        emailMessage.From.Add(new MailboxAddress(senderName, senderEmail));
        emailMessage.To.Add(new MailboxAddress("", destinatario));
        emailMessage.Subject = asunto;

        // 1. Construir el cuerpo del mensaje
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"<html><body>{cuerpo.Replace("\n", "<br>")}</body></html>";

        // 2. Adjuntar el archivo desde la Base de Datos (Bytes)
        if (contenidoAdjunto != null && contenidoAdjunto.Length > 0 && !string.IsNullOrEmpty(nombreAdjunto))
        {
            // Agregamos el archivo PDF usando los bytes
            bodyBuilder.Attachments.Add(nombreAdjunto, contenidoAdjunto, ContentType.Parse("application/pdf"));
        }

        var messageBody = bodyBuilder.ToMessageBody();

        // 3. FIRMA DIGITAL (Opcional)
        if (certificadoStream != null && !string.IsNullOrEmpty(passwordCertificado))
        {
            try
            {
                // A) Leemos el certificado a un MemoryStream
                using var memoryStream = new MemoryStream();
                await certificadoStream.CopyToAsync(memoryStream);
                var certBytes = memoryStream.ToArray();

                var flags = X509KeyStorageFlags.MachineKeySet |
                            X509KeyStorageFlags.EphemeralKeySet |
                            X509KeyStorageFlags.Exportable;

                // B) AQUÍ DECLARAMOS EL 'SIGNER' QUE FALTABA
                var certificado = new X509Certificate2(certBytes, passwordCertificado.Trim(), flags);
                var signer = new CmsSigner(certificado);

                signer.DigestAlgorithm = DigestAlgorithm.Sha256;

                // C) Usamos el contexto temporal para firmar
                using (var ctx = new TemporarySecureMimeContext())
                {
                    // Ahora 'signer' SÍ existe en este contexto
                    emailMessage.Body = MultipartSigned.Create(ctx, signer, messageBody);
                }
            }
            catch (Exception ex)
            {
                // Si falla la firma, lanzamos error para que te enteres
                throw new Exception($"Error al firmar digitalmente: {ex.Message}");
            }
        }
        else
        {
            // Si no hay certificado, enviamos sin firmar
            emailMessage.Body = messageBody;
        }

        // 4. Enviar correo SMTP
        using (var client = new SmtpClient())
        {
            try
            {
                var server = _config["EmailSettings:Server"];
                var port = int.Parse(_config["EmailSettings:Port"]);
                var password = _config["EmailSettings:Password"];

                client.CheckCertificateRevocation = false;

                await client.ConnectAsync(server, port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(senderEmail, password);
                await client.SendAsync(emailMessage);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}