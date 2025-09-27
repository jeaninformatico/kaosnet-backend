using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Suscripcione
{
    public int IdSuscripcion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaRenovacion { get; set; }

    public string? CicloPago { get; set; }

    public decimal? PrecioUsd { get; set; }

    public decimal? PrecioLocal { get; set; }

    public string? Estado { get; set; }

    public int? IdCuenta { get; set; }

    public int? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Cuenta? IdCuentaNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
