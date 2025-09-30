using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? ContactEmail { get; set; }

    public string? ContactNumber { get; set; }

    public int? SubscriptionPlanFk { get; set; }

    public string? CustomerStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<SalesAccount> SalesAccounts { get; set; } = new List<SalesAccount>();

    public virtual SubscriptionPlan? SubscriptionPlanFkNavigation { get; set; }
}
