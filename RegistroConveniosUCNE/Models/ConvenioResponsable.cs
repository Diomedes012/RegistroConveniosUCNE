using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionConveniosUCNE.Models;

public class ConvenioResponsable
{
    [Key]
    public int ConvenioResponsableId { get; set; }

    [ForeignKey("Convenio")]
    public int IdConvenio { get; set; }
    public Convenio? Convenio { get; set; }

    [ForeignKey("Responsable")]
    public int IdResponsable { get; set; }
    public Responsable? Responsable { get; set; }

    [MaxLength(100)]
    public string? Rol { get; set; }
}

