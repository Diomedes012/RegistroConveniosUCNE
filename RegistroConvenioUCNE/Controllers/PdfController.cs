using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroConvenioUCNE.Data;

namespace RegistroConvenioUCNE.Controllers;

[Route("api/pdf")]
[ApiController]
public class PdfController : ControllerBase
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public PdfController(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPdf(int id)
    {
        using var context = await _dbFactory.CreateDbContextAsync();

        // Solo traemos los bytes del archivo que necesitamos (Eficiencia)
        var convenio = await context.Convenio
            .Where(c => c.IdConvenio == id)
            .Select(c => new { c.ContenidoArchivo, c.NombreArchivo })
            .FirstOrDefaultAsync();

        if (convenio == null || convenio.ContenidoArchivo == null)
        {
            return NotFound("Archivo no encontrado");
        }

        // Esto devuelve el archivo como si fuera una descarga real del servidor
        return File(convenio.ContenidoArchivo, "application/pdf");
    }
}