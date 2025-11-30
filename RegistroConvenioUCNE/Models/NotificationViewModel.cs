using System;

namespace RegistroConvenioUCNE.Models;

public class NotificacionViewModel
{
    // Datos del negocio (Cruce entre Convenio y Alerta)
    public int? AlertaId { get; set; }
    public int ConvenioId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = ""; // "Vence en X días" o "Vencido"
    public DateTime Date { get; set; } // Fecha de vencimiento
    public string Estado { get; set; } = "";
    public string ArchivoPrincipal { get; set; } = "";
    public string AdminComment { get; set; } = "";

    // Propiedades exclusivas de la Interfaz (UI)
    public bool IsFavorite { get; set; }
    public bool IsSelected { get; set; }
}