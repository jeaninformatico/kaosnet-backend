using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class Password
{
    public int PasswordId { get; set; }

    public string Password1 { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int UserFk { get; set; }

    public string? PasswordStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual User UserFkNavigation { get; set; } = null!;
}
