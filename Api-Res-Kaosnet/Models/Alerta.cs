using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Alerta
{
    public int IdAlerta { get; set; }

    public int? DiasRestantes { get; set; }

    public string? EstadoAlerta { get; set; }

    public int? IdCuenta { get; set; }

    public virtual Cuenta? IdCuentaNavigation { get; set; }
}
