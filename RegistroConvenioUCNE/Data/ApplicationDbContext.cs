using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistroConvenioUCNE.Models;

namespace RegistroConvenioUCNE.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
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

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Decanato",
                    NormalizedName = "DECANATO"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Digitador",
                    NormalizedName = "DIGITADOR"
                }
            );
        }
    }
}
