using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class RoleAccess
{
    public int RoleAccessId { get; set; }

    public int RoleFk { get; set; }

    public int ViewFk { get; set; }

    public sbyte? IsWrite { get; set; }

    public string? RoleAccessStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual Role RoleFkNavigation { get; set; } = null!;

    public virtual View ViewFkNavigation { get; set; } = null!;
}
