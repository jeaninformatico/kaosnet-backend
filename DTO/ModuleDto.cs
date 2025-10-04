namespace Api_Kaos_Net.DTO
{
    public class ModuleDto
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; } = null!;
        public string? ModuleDescription { get; set; }
        public string? ModuleIcon { get; set; }
        public int? MenuSequence { get; set; }
        public string? ModuleStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
