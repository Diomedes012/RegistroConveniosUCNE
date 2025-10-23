using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionConveniosUCNE.Models;

public class Responsable
{
    [Key]
    public int IdResponsable { get; set; }

    [Required, MaxLength(150)]
    public string Nombre { get; set; }

    [MaxLength(100)]
    public string? Cargo { get; set; }

    [MaxLength(50)]
    public string? Telefono { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(150)]
    public string? Departamento { get; set; }

    public ICollection<ConvenioResponsable> ConvenioResponsables { get; set; } = new List<ConvenioResponsable>();
}

