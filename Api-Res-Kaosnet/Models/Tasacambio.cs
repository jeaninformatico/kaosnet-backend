using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Tasacambio
{
    public int Idtasa { get; set; }

    public DateOnly? Fecha { get; set; }

    public decimal? TasaUsd { get; set; }

    public string? Fuente { get; set; }

    public bool? Activa { get; set; }
}
