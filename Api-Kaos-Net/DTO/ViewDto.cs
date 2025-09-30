namespace Api_Kaos_Net.DTO
{
    public class ViewDto
    {
        public int ViewId { get; set; }
        public string ViewName { get; set; } = null!;
        public string? ViewDescription { get; set; }
        public string? ViewIcon { get; set; }
        public int ModuleFk { get; set; }
        public int? ModuleSequence { get; set; }
        public string? ViewPath { get; set; }
        public int? ParentViewFk { get; set; }
        public string? ViewStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
