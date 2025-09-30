namespace Api_Kaos_Net.DTO
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public string? RoleStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
