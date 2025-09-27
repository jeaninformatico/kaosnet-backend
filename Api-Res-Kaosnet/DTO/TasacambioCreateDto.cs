// TasacambioCreateDto.cs
namespace Api_Res_Kaosnet.DTO
{
    public class TasacambioCreateDto
    {
        public DateOnly? Fecha { get; set; }
        public decimal? TasaUsd { get; set; }
        public string? Fuente { get; set; }
        public bool? Activa { get; set; }
    }
}
