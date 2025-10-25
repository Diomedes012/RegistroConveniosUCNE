using GestionConveniosUCNE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionConveniosUCNE.Services;

public class ActividadesService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int actividadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Actividad.AnyAsync(a => a.IdActividad == actividadId);
    }

    private async Task<bool> Insertar(Actividad actividad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Actividad.Add(actividad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Actividad actividad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(actividad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Actividad actividad)
    {
        if (!await Existe(actividad.IdActividad))
            return await Insertar(actividad);
        else
            return await Modificar(actividad);
    }

    public async Task<Actividad?> Buscar(int actividadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Actividad
            .Include(a => a.Convenio)
            .FirstOrDefaultAsync(a => a.IdActividad == actividadId);
    }

    public async Task<bool> Eliminar(int actividadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var actividad = await contexto.Actividad.FindAsync(actividadId);

        if (actividad == null)
            return false;

        contexto.Actividad.Remove(actividad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Actividad>> Listar(Expression<Func<Actividad, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Actividad
            .Include(a => a.Convenio)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

