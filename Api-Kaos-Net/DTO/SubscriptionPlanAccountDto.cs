namespace Api_Kaos_Net.DTO
{
    public class SubscriptionPlanAccountDto
    {
        public int SubscriptionPlanAccountId { get; set; }
        public int SubscriptionPlanFk { get; set; }
        public int StreamingAccountFk { get; set; }
        public int? QuantityAccounts { get; set; }
        public decimal? AmountSubTotal { get; set; }
        public string? SubscriptionPlanAccountStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
