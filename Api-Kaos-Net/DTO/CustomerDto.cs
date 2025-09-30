namespace Api_Kaos_Net.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public int? SubscriptionPlanFk { get; set; }
        public string? CustomerStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
