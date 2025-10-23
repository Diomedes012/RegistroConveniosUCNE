using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionConveniosUCNE.Models;

public class Rol
{
    [Key]
    public int IdRol { get; set; }

    [Required, MaxLength(100)]
    public string NombreRol { get; set; }

    [MaxLength(255)]
    public string Descripcion { get; set; }

    public ICollection<Usuario> Usuarios { get; set; }
}

