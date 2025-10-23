using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionConveniosUCNE.Models;

public class Alerta
{
    [Key]
    public int IdAlerta { get; set; }

    [ForeignKey("Convenio")]
    public int IdConvenio { get; set; }
    public Convenio Convenio { get; set; }

    public DateTime FechaGenerada { get; set; } = DateTime.Now;
    public DateTime? FechaEnvio { get; set; }

    public int DiasAnticipacion { get; set; } = 30;

    [MaxLength(50)]
    public string EstadoAlerta { get; set; } = "Pendiente";

    [MaxLength(500)]
    public string Destinatarios { get; set; }
}

