using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class SubscriptionPlan
{
    public int SubscriptionPlanId { get; set; }

    public string? SubscriptionPlanName { get; set; }

    public string? SubscriptionPlanDescription { get; set; }

    public decimal? AmountTotal { get; set; }

    public string? SubscriptionPlanStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<SubscriptionPlanAccount> SubscriptionPlanAccounts { get; set; } = new List<SubscriptionPlanAccount>();
}
