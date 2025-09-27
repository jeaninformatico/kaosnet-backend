using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Egreso
{
    public int Idegreso { get; set; }

    public decimal? Monto { get; set; }

    public string? Moneda { get; set; }

    public DateOnly? FechaPago { get; set; }

    public string? Proveedor { get; set; }

    public int? IdServicio { get; set; }

    public virtual Servicio? IdServicioNavigation { get; set; }
}
