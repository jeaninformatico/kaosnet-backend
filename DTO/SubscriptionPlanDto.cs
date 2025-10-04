namespace Api_Kaos_Net.DTOs
{
    public class SubscriptionPlanDto
    {
        public int SubscriptionPlanId { get; set; }
        public string? SubscriptionPlanName { get; set; }
        public string? SubscriptionPlanDescription { get; set; }
        public decimal? AmountTotal { get; set; }
        public string? SubscriptionPlanStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
