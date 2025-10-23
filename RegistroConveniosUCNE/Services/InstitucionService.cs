using GestionConveniosUCNE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionConveniosUCNE.Services;

public class InstitucionesService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int institucionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Instituciones.AnyAsync(i => i.IdInstitucion == institucionId);
    }

    private async Task<bool> Insertar(Institucion institucion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Instituciones.Add(institucion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Institucion institucion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(institucion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Institucion institucion)
    {
        if (!await Existe(institucion.IdInstitucion))
            return await Insertar(institucion);
        else
            return await Modificar(institucion);
    }

    public async Task<Institucion?> Buscar(int institucionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Instituciones
            .Include(i => i.ConvenioInstituciones)
                .ThenInclude(ci => ci.Convenio)
            .FirstOrDefaultAsync(i => i.IdInstitucion == institucionId);
    }

    public async Task<bool> Eliminar(int institucionId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var institucion = await contexto.Instituciones
            .Include(i => i.ConvenioInstituciones)
            .FirstOrDefaultAsync(i => i.IdInstitucion == institucionId);

        if (institucion == null)
            return false;

        contexto.ConvenioInstituciones.RemoveRange(institucion.ConvenioInstituciones);
        contexto.Instituciones.Remove(institucion);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Institucion>> Listar(Expression<Func<Institucion, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Instituciones
            .Include(i => i.ConvenioInstituciones)
                .ThenInclude(ci => ci.Convenio)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

