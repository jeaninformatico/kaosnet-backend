namespace Api_Kaos_Net.DTO
{
    public class RoleAccessDto
    {
        public int RoleAccessId { get; set; }
        public int RoleFk { get; set; }
        public int ViewFk { get; set; }
        public bool? IsWrite { get; set; }
        public string? RoleAccessStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
