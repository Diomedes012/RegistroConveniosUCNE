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

        if (alerta == null)
            return false;

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

        int diasAnticipacion = 30;
        DateTime fechaLimite = DateTime.Now.AddDays(diasAnticipacion);

        var conveniosPorVencer = await contexto.Convenio
            .Where(c => c.Estado == "Activo" && c.FechaVencimiento <= fechaLimite)
            .AsNoTracking()
            .ToListAsync();

        bool cambiosRealizados = false;

        foreach (var convenio in conveniosPorVencer)
        {
            bool existeAlerta = await contexto.Alerta
                .AnyAsync(a => a.IdConvenio == convenio.IdConvenio && a.EstadoAlerta == "Pendiente");

            if (!existeAlerta)
            {
                var nuevaAlerta = new Alerta
                {
                    IdConvenio = convenio.IdConvenio,
                    FechaGenerada = DateTime.Now,
                    DiasAnticipacion = diasAnticipacion,
                    EstadoAlerta = "Pendiente",
                    Destinatarios = "Administración",
                    FechaEnvio = null
                };

                contexto.Alerta.Add(nuevaAlerta);
                cambiosRealizados = true;
            }
        }

        if (cambiosRealizados)
        {
            await contexto.SaveChangesAsync();
        }
    }
}
