namespace Api_Kaos_Net.DTOs
{
    public class StreamingAccountDto
    {
        public int StreamingAccountId { get; set; }
        public string? StreamingAccountName { get; set; }
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public decimal? Price { get; set; }
        public int StreamingTypeFk { get; set; }
        public DateTime ValidDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? MaximumQuantityProfiles { get; set; }
        public int? ProfilesQuantity { get; set; }
        public string? StreamingAccountStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
