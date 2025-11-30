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
    string? rutaArchivoAdjunto,
    Stream? certificadoStream = null,
    string? passwordCertificado = null)
    {
        var emailMessage = new MimeMessage();

        // Configuración básica
        var senderName = _config["EmailSettings:SenderName"];
        var senderEmail = _config["EmailSettings:SenderEmail"];
        emailMessage.From.Add(new MailboxAddress(senderName, senderEmail));
        emailMessage.To.Add(new MailboxAddress("", destinatario));
        emailMessage.Subject = asunto;

        // 1. Construir Body
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = $@"<html><body>{cuerpo.Replace("\n", "<br>")}</body></html>";

        if (!string.IsNullOrEmpty(rutaArchivoAdjunto) && File.Exists(rutaArchivoAdjunto))
        {
            await bodyBuilder.Attachments.AddAsync(rutaArchivoAdjunto);
        }

        var messageBody = bodyBuilder.ToMessageBody();

        // 2. FIRMA ELECTRÓNICA (Corregida para evitar error de SQLite)
        if (certificadoStream != null && !string.IsNullOrEmpty(passwordCertificado))
        {
            try
            {
                // A) Preparamos el certificado
                using var memoryStream = new MemoryStream();
                await certificadoStream.CopyToAsync(memoryStream);
                var certBytes = memoryStream.ToArray();

                var flags = X509KeyStorageFlags.MachineKeySet |
                            X509KeyStorageFlags.EphemeralKeySet |
                            X509KeyStorageFlags.Exportable;

                var certificado = new X509Certificate2(certBytes, passwordCertificado.Trim(), flags);
                var signer = new CmsSigner(certificado);

                // Define explícitamente el algoritmo para evitar búsquedas innecesarias
                signer.DigestAlgorithm = DigestAlgorithm.Sha256;

                // B) SOLUCIÓN AL ERROR SQLITE:
                // Creamos un contexto temporal en memoria RAM.
                // Esto evita que MimeKit intente buscar/crear una base de datos SQLite.
                using (var ctx = new TemporarySecureMimeContext())
                {
                    // Pasamos el contexto 'ctx' como primer parámetro
                    emailMessage.Body = MultipartSigned.Create(ctx, signer, messageBody);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al firmar (Crypto): {ex.Message}");
            }
        }
        else
        {
            emailMessage.Body = messageBody;
        }

        // 3. Enviar
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