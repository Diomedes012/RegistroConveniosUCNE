using Microsoft.EntityFrameworkCore;
using RegistroConvenioUCNE.Data;
using RegistroConvenioUCNE.Models;
using System.Linq.Expressions;

namespace RegistroConvenioUCNE.Services;

public class ConveniosService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    private async Task<bool> Existe(int convenioId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Convenio.AnyAsync(c => c.IdConvenio == convenioId);
    }

    private async Task<bool> Insertar(Convenio convenio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Convenio.Add(convenio);
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

        return await contexto.Convenio
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
        var convenio = await contexto.Convenio
            .Include(c => c.ConvenioInstituciones)
            .Include(c => c.ConvenioResponsables)
            .FirstOrDefaultAsync(c => c.IdConvenio == convenioId);

        if (convenio == null)
            return false;

        contexto.ConvenioInstitucion.RemoveRange(convenio.ConvenioInstituciones);
        contexto.ConvenioResponsable.RemoveRange(convenio.ConvenioResponsables);
        contexto.Convenio.Remove(convenio);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Convenio>> Listar(Expression<Func<Convenio, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Convenio
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

