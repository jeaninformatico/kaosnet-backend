using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class Module
{
    public int ModuleId { get; set; }

    public string ModuleName { get; set; } = null!;

    public string? ModuleDescription { get; set; }

    public string? ModuleIcon { get; set; }

    public int? MenuSequence { get; set; }

    public string? ModuleStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<View> Views { get; set; } = new List<View>();
}
