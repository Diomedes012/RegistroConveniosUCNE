using GestionConveniosUCNE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroConveniosUCNE.Services;

public class NotificacionesService(IDbContextFactory<Contexto> factory)
{
    private async Task<bool> Existe(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Alerta.AnyAsync(a => a.IdAlerta == id);
    }

    private async Task<bool> Insertar(Alerta alerta)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        contexto.Alerta.Add(alerta);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Alerta alerta)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        contexto.Update(alerta);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Alerta alerta)
    {
        if (!await Existe(alerta.IdAlerta))
            return await Insertar(alerta);
        else
            return await Modificar(alerta);
    }

    public async Task<Alerta?> Buscar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Alerta
            .Include(a => a.Convenio)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.IdAlerta == id);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        var alerta = await contexto.Alerta.FindAsync(id);

        if (alerta == null) return false;

        contexto.Alerta.Remove(alerta);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Alerta>> Listar(Expression<Func<Alerta, bool>> criterio)
    {
        await using var contexto = await factory.CreateDbContextAsync();
        return await contexto.Alerta
            .Include(a => a.Convenio)
            .Where(criterio)
            .OrderByDescending(a => a.FechaGenerada)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task GenerarAlertasAutomaticas()
    {
        await using var contexto = await factory.CreateDbContextAsync();

        int diasAnticipacion = 5000;
        DateTime fechaLimite = DateTime.Now.AddDays(diasAnticipacion);

        // 1. Obtener TODOS los convenios candidatos en UNA sola consulta
        // (Estado Activo y que venzan antes de la fecha límite)
        var conveniosPorVencer = await contexto.Convenio
            .Where(c => c.Estado == "Activo" && c.FechaVencimiento <= fechaLimite)
            .AsNoTracking()
            .ToListAsync();

        if (!conveniosPorVencer.Any()) return;

        var idsConvenios = conveniosPorVencer.Select(c => c.IdConvenio).ToList();

        // Traemos de la BD solo los IDs de Convenios que YA tienen una alerta "Pendiente"
        var idsConAlerta = await contexto.Alerta
            .Where(a => idsConvenios.Contains(a.IdConvenio) && a.EstadoAlerta == "Pendiente")
            .Select(a => a.IdConvenio)
            .ToListAsync();

        // Convertimos a HashSet para que la comparación sea instantánea
        var setAlertasExistentes = new HashSet<int>(idsConAlerta);

        var nuevasAlertas = new List<Alerta>();

        // 3. Crear objetos en memoria solo para los que faltan
        foreach (var convenio in conveniosPorVencer)
        {
            // Si el ID del convenio NO está en el set de alertas existentes, creamos una nueva
            if (!setAlertasExistentes.Contains(convenio.IdConvenio))
            {
                nuevasAlertas.Add(new Alerta
                {
                    IdConvenio = convenio.IdConvenio,
                    FechaGenerada = DateTime.Now,
                    DiasAnticipacion = diasAnticipacion,
                    EstadoAlerta = "Pendiente",
                    Destinatarios = "Administración",
                    FechaEnvio = null
                });
            }
        }

        // 4. Guardado masivo (Mucho más rápido que guardar uno por uno)
        if (nuevasAlertas.Any())
        {
            contexto.Alerta.AddRange(nuevasAlertas);
            await contexto.SaveChangesAsync();
        }
    }
}