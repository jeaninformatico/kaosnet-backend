using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class StreamingAccount
{
    public int StreamingAccountId { get; set; }

    public string? StreamingAccountName { get; set; }

    public string AccountEmail { get; set; } = null!;

    public string AccountPassword { get; set; } = null!;

    public decimal? Price { get; set; }

    public int StreamingTypeFk { get; set; }

    public DateTime ValidDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public int? MaximumQuantityProfiles { get; set; }

    public int? ProfilesQuantity { get; set; }

    public string? StreamingAccountStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<SalesAccount> SalesAccounts { get; set; } = new List<SalesAccount>();

    public virtual StreamingType StreamingTypeFkNavigation { get; set; } = null!;

    public virtual ICollection<SubscriptionPlanAccount> SubscriptionPlanAccounts { get; set; } = new List<SubscriptionPlanAccount>();
}
