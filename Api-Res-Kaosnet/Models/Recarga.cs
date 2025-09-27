using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Recarga
{
    public int IdRecarga { get; set; }

    public string? TipoJuego { get; set; }

    public decimal? MontoUsd { get; set; }

    public decimal? TasaCambio { get; set; }

    public decimal? MontoBs { get; set; }

    public DateOnly? FechaRecarga { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
