using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class View
{
    public int ViewId { get; set; }

    public string ViewName { get; set; } = null!;

    public string? ViewDescription { get; set; }

    public string? ViewIcon { get; set; }

    public int ModuleFk { get; set; }

    public int? ModuleSequence { get; set; }

    public string? ViewPath { get; set; }

    public int? ParentViewFk { get; set; }

    public string? ViewStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<View> InverseParentViewFkNavigation { get; set; } = new List<View>();

    public virtual Module ModuleFkNavigation { get; set; } = null!;

    public virtual View? ParentViewFkNavigation { get; set; }

    public virtual ICollection<RoleAccess> RoleAccesses { get; set; } = new List<RoleAccess>();
}
