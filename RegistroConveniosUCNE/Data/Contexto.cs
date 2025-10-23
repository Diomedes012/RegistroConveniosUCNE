using Microsoft.EntityFrameworkCore;

namespace GestionConveniosUCNE.Models;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) 
    { 
    }

    public DbSet<Rol> Roles { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Institucion> Instituciones { get; set; }
    public DbSet<Convenio> Convenios { get; set; }
    public DbSet<ConvenioInstitucion> ConvenioInstituciones { get; set; }
    public DbSet<Responsable> Responsables { get; set; }
    public DbSet<ConvenioResponsable> ConvenioResponsables { get; set; }
    public DbSet<Actividad> Actividades { get; set; }
    public DbSet<Alerta> Alertas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}

