using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionConveniosUCNE.Models;

public class Convenio
{
    [Key]
    public int IdConvenio { get; set; }

    [Required, MaxLength(200)]
    public string Titulo { get; set; }

    public string? DescripcionObjetivos { get; set; }

    [MaxLength(100)]
    public string? TipoConvenio { get; set; }

    [MaxLength(100)]
    public string? Categoria { get; set; }

    public DateTime FechaFirma { get; set; }
    public DateTime? FechaVencimiento { get; set; }

    public int? DuracionMeses { get; set; }

    [MaxLength(50)]
    public string Estado { get; set; } = "Activo";

    [MaxLength(255)]
    public string? ArchivoPrincipal { get; set; }

    [ForeignKey("Usuario")]
    public int? CreadoPor { get; set; }
    public Usuario? Usuario { get; set; }

    public DateTime UltimaModificacion { get; set; } = DateTime.Now;

    // Relaciones
    public ICollection<ConvenioInstitucion> ConvenioInstituciones { get; set; } = new List<ConvenioInstitucion>();
    public ICollection<ConvenioResponsable> ConvenioResponsables { get; set; } = new List<ConvenioResponsable>();
    public ICollection<Actividad> Actividades { get; set; } = new List<Actividad>();
    public ICollection<Alerta> Alertas { get; set; } = new List<Alerta>();
}


