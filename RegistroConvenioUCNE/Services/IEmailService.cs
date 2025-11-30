namespace RegistroConvenioUCNE.Services;

public interface IEmailService
{
    Task EnviarCorreoConAdjuntoAsync(
            string destinatario,
            string asunto,
            string cuerpo,
            string? rutaArchivoAdjunto,
            Stream? certificadoStream = null,
            string? passwordCertificado = null
        );
}

