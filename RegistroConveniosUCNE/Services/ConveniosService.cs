using GestionConveniosUCNE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionConveniosUCNE.Services;

public class ConveniosService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int convenioId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Convenios.AnyAsync(c => c.IdConvenio == convenioId);
    }

    private async Task<bool> Insertar(Convenio convenio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Convenios.Add(convenio);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Convenio convenio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(convenio);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Convenio convenio)
    {
        if (!await Existe(convenio.IdConvenio))
            return await Insertar(convenio);
        else
            return await Modificar(convenio);
    }

    public async Task<Convenio?> Buscar(int convenioId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Convenios
            .Include(c => c.ConvenioInstituciones)
                .ThenInclude(ci => ci.Institucion)
            .Include(c => c.ConvenioResponsables)
                .ThenInclude(cr => cr.Responsable)
            .Include(c => c.Usuario)
            .FirstOrDefaultAsync(c => c.IdConvenio == convenioId);
    }

    public async Task<bool> Eliminar(int convenioId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var convenio = await contexto.Convenios
            .Include(c => c.ConvenioInstituciones)
            .Include(c => c.ConvenioResponsables)
            .FirstOrDefaultAsync(c => c.IdConvenio == convenioId);

        if (convenio == null)
            return false;

        contexto.ConvenioInstituciones.RemoveRange(convenio.ConvenioInstituciones);
        contexto.ConvenioResponsables.RemoveRange(convenio.ConvenioResponsables);
        contexto.Convenios.Remove(convenio);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Convenio>> Listar(Expression<Func<Convenio, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Convenios
            .Include(c => c.ConvenioInstituciones)
                .ThenInclude(ci => ci.Institucion)
            .Include(c => c.ConvenioResponsables)
                .ThenInclude(cr => cr.Responsable)
            .Include(c => c.Usuario)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

