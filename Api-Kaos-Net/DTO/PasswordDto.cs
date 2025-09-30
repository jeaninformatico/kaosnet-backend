namespace Api_Kaos_Net.DTOs
{
    public class PasswordDto
    {
        public int PasswordId { get; set; }
        public string Password1 { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int UserFk { get; set; }
        public string? PasswordStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
