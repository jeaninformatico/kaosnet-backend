namespace Api_Kaos_Net.DTOs
{
    public class ConversionRateDto
    {
        public int ConversionRateId { get; set; }
        public DateTime ValidDate { get; set; }
        public decimal AmountRate { get; set; }
        public int CurrencyFromId { get; set; }
        public int CurrencyToId { get; set; }
        public bool? IsReversed { get; set; }
        public int? ParentConversionRateFk { get; set; }
        public string? ConversionRateStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
