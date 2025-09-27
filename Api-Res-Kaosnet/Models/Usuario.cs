using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string ClaveHash { get; set; } = null!;

    public string? ImagenUrl { get; set; }

    public string Telefono { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdRol { get; set; }

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Recarga> Recargas { get; set; } = new List<Recarga>();
}
