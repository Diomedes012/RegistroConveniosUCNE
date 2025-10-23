using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionConveniosUCNE.Models;

public class Institucion
{
    [Key]
    public int IdInstitucion { get; set; }

    [Required, MaxLength(200)]
    public string Nombre { get; set; }

    [MaxLength(200)]
    public string? Direccion { get; set; }

    [MaxLength(50)]
    public string? Telefono { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(100)]
    public string? TipoInstitucion { get; set; }

    public ICollection<ConvenioInstitucion> ConvenioInstituciones { get; set; } = new List<ConvenioInstitucion>();
}

