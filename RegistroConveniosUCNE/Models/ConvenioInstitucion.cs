using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionConveniosUCNE.Models;

public class ConvenioInstitucion
{
    [Key]
    public int ConvenioInstitucionId { get; set; }

    [ForeignKey("Convenio")]
    public int IdConvenio { get; set; }
    public Convenio? Convenio { get; set; }

    [ForeignKey("Institucion")]
    public int IdInstitucion { get; set; }
    public Institucion? Institucion { get; set; }

    [MaxLength(100)]
    public string? Rol { get; set; }
}
