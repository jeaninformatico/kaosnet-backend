using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? RoleDescription { get; set; }

    public string? RoleStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<RoleAccess> RoleAccesses { get; set; } = new List<RoleAccess>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
