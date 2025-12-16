public interface IEmailService
{
    Task EnviarCorreoConAdjuntoAsync(
        string destinatario,
        string asunto,
        string cuerpo,
        byte[]? contenidoAdjunto = null,
        string? nombreAdjunto = null,
        Stream? certificadoStream = null,
        string? passwordCertificado = null
    );
}