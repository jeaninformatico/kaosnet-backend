using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class SalesAccount
{
    public int IdSalesAccount { get; set; }

    public string? ContactNumber { get; set; }

    public string? ContactEmail { get; set; }

    public DateTime? SalesDate { get; set; }

    public string? ProfilePin { get; set; }

    public int? ProfileNumber { get; set; }

    public decimal? AmountSales { get; set; }

    public int? CustomerId { get; set; }

    public int StreamingAccountFk { get; set; }

    public int CurrencyFk { get; set; }

    public string? SalesAccountStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual Currency CurrencyFkNavigation { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual StreamingAccount StreamingAccountFkNavigation { get; set; } = null!;
}
