namespace Api_Kaos_Net.DTOs
{
    public class StreamingTypeDto
    {
        public int StreamingTypeId { get; set; }
        public string? StreamingTypeName { get; set; }
        public string? StreamingTypeDescription { get; set; }
        public string? StreamingTypeStatus { get; set; }
        public bool IsActive { get; set; }
        public int? Idsession { get; set; }
    }
}
