namespace Api_Kaos_Net.DTOs
{
    public class SalesAccountDto
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
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
