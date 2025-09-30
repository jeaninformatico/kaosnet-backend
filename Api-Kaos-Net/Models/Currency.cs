using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class Currency
{
    public int CurrencyId { get; set; }

    public string CurrencyName { get; set; } = null!;

    public string? CurrencyCode { get; set; }

    public string? Symbol { get; set; }

    public string? CurrencyStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual ICollection<ConversionRate> ConversionRateCurrencyFroms { get; set; } = new List<ConversionRate>();

    public virtual ICollection<ConversionRate> ConversionRateCurrencyTos { get; set; } = new List<ConversionRate>();

    public virtual ICollection<SalesAccount> SalesAccounts { get; set; } = new List<SalesAccount>();
}
