using GestionConveniosUCNE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionConveniosUCNE.Services;

public class ResponsablesService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int responsableId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Responsable.AnyAsync(r => r.IdResponsable == responsableId);
    }

    private async Task<bool> Insertar(Responsable responsable)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Responsable.Add(responsable);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Responsable responsable)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(responsable);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Responsable responsable)
    {
        if (!await Existe(responsable.IdResponsable))
            return await Insertar(responsable);
        else
            return await Modificar(responsable);
    }

    public async Task<Responsable?> Buscar(int responsableId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Responsable
            .Include(r => r.ConvenioResponsables)
                .ThenInclude(cr => cr.Convenio)
            .FirstOrDefaultAsync(r => r.IdResponsable == responsableId);
    }

    public async Task<bool> Eliminar(int responsableId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var responsable = await contexto.Responsable
            .Include(r => r.ConvenioResponsables)
            .FirstOrDefaultAsync(r => r.IdResponsable == responsableId);

        if (responsable == null)
            return false;

        contexto.ConvenioResponsable.RemoveRange(responsable.ConvenioResponsables);
        contexto.Responsable.Remove(responsable);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Responsable>> Listar(Expression<Func<Responsable, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Responsable
            .Include(r => r.ConvenioResponsables)
                .ThenInclude(cr => cr.Convenio)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

