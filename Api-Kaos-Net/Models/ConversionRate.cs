using System;
using System.Collections.Generic;

namespace Api_Kaos_Net.Models;

public partial class ConversionRate
{
    public int ConversionRateId { get; set; }

    public DateTime ValidDate { get; set; }

    public decimal AmountRate { get; set; }

    public int CurrencyFromId { get; set; }

    public int CurrencyToId { get; set; }

    public sbyte? IsReversed { get; set; }

    public int? ParentConversionRateFk { get; set; }

    public string? ConversionRateStatus { get; set; }

    public sbyte IsActive { get; set; }

    public int? Idsession { get; set; }

    public virtual Currency CurrencyFrom { get; set; } = null!;

    public virtual Currency CurrencyTo { get; set; } = null!;

    public virtual ICollection<ConversionRate> InverseParentConversionRateFkNavigation { get; set; } = new List<ConversionRate>();

    public virtual ConversionRate? ParentConversionRateFkNavigation { get; set; }
}
