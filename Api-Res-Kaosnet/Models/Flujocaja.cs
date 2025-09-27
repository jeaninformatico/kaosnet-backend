using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Flujocaja
{
    public int Idflujo { get; set; }

    public DateOnly? Fecha { get; set; }

    public decimal? IngresosUsd { get; set; }

    public decimal? IngresosBs { get; set; }

    public decimal? EgresosUsd { get; set; }

    public decimal? EgresosBs { get; set; }

    public decimal? BalanceUsd { get; set; }

    public decimal? BalanceBs { get; set; }
}
