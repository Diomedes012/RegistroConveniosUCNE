using Microsoft.EntityFrameworkCore;

namespace GestionConveniosUCNE.Models;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) 
    { 
    }

    public DbSet<Rol> Rol { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Institucion> Institucion { get; set; }
    public DbSet<Convenio> Convenio { get; set; }
    public DbSet<ConvenioInstitucion> ConvenioInstitucion { get; set; }
    public DbSet<Responsable> Responsable { get; set; }
    public DbSet<ConvenioResponsable> ConvenioResponsable { get; set; }
    public DbSet<Actividad> Actividad { get; set; }
    public DbSet<Alerta> Alerta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

