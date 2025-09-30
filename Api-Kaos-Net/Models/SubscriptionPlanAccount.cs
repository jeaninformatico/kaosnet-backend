using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class SubscriptionPlanAccount
{
    public int SubscriptionPlanAccountId { get; set; }

    public int SubscriptionPlanFk { get; set; }

    public int StreamingAccountFk { get; set; }

    public int? QuantityAccounts { get; set; }

    public decimal? AmountSubTotal { get; set; }

    public string? SubscriptionPlanAccountStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual StreamingAccount StreamingAccountFkNavigation { get; set; } = null!;

    public virtual SubscriptionPlan SubscriptionPlanFkNavigation { get; set; } = null!;
}
