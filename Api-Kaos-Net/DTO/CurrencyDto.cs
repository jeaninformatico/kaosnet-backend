namespace Api_Kaos_Net.DTOs
{
    public class CurrencyDto
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; } = null!;
        public string? CurrencyCode { get; set; }
        public string? Symbol { get; set; }
        public string? CurrencyStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
