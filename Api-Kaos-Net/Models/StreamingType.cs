using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class StreamingType
{
    public int StreamingTypeId { get; set; }

    public string? StreamingTypeName { get; set; }

    public string? StreamingTypeDescription { get; set; }

    public string? StreamingTypeStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<StreamingAccount> StreamingAccounts { get; set; } = new List<StreamingAccount>();
}
