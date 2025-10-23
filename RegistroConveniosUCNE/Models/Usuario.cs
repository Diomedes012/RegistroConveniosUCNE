using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionConveniosUCNE.Models;

public class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [Required, MaxLength(150)]
    public string Nombre { get; set; }

    [Required, MaxLength(100)]
    public string UsuarioLogin { get; set; }

    [Required, MaxLength(255)]
    public string ContrasenaHash { get; set; }

    [MaxLength(150)]
    public string EmailInstitucional { get; set; }

    public bool Activo { get; set; } = true;

    [ForeignKey("Rol")]
    public int IdRol { get; set; }
    public Rol Rol { get; set; }
}


