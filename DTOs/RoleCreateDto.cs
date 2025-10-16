namespace Api_Kaos_Net.DTO
{
    public class RoleCreateDto
    {
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public string? RoleStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
