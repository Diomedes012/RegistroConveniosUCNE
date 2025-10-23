using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionConveniosUCNE.Models;

public class Actividad
{
    [Key]
    public int IdActividad { get; set; }

    [ForeignKey("Convenio")]
    public int IdConvenio { get; set; }
    public Convenio Convenio { get; set; }

    [MaxLength(200)]
    public string TituloActividad { get; set; }

    public string Descripcion { get; set; }

    public DateTime? FechaActividad { get; set; }

    [MaxLength(255)]
    public string EvidenciaArchivo { get; set; }

    [ForeignKey("Usuario")]
    public int? CreadoPor { get; set; }
    public Usuario Usuario { get; set; }
}
