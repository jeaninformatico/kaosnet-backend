using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public string? MetodoPago { get; set; }

    public decimal? MontoUsd { get; set; }

    public string? Banco { get; set; }

    public string? Telefono { get; set; }

    public string? Cedula { get; set; }

    public decimal? MontoLocal { get; set; }

    public decimal? TasaUsd { get; set; }

    public DateTime? FechaPago { get; set; }

    public string? Referencia { get; set; }

    public int? IdSuscripcion { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Suscripcione? IdSuscripcionNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
