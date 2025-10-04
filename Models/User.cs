using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public int? SessionTime { get; set; }

    public int? FailedLogin { get; set; }

    public int? CurrentSessions { get; set; }

    public string Email { get; set; } = null!;

    public string? SecurityQuestion { get; set; }

    public string? SecretAnswer { get; set; }

    public int RoleFk { get; set; }

    public string? UserStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<Password> Passwords { get; set; } = new List<Password>();

    public virtual Role RoleFkNavigation { get; set; } = null!;
}
