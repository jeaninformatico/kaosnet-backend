using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Tipo { get; set; }

    public string? ImagenUrl { get; set; }

    public string? PaísOrigen { get; set; }

    public string? MonedaBae { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();

    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();
}
