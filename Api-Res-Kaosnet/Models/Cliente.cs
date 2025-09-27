using System;
using System.Collections.Generic;

namespace Api_Res_Kaosnet.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Contacto { get; set; }

    public string? País { get; set; }

    public virtual ICollection<Suscripcione> Suscripciones { get; set; } = new List<Suscripcione>();
}
