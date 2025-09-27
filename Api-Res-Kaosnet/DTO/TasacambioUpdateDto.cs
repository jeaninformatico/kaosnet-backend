// TasacambioUpdateDto.cs
namespace Api_Res_Kaosnet.DTO
{
    public class TasacambioUpdateDto
    {
        public int Idtasa { get; set; }
        public DateOnly? Fecha { get; set; }
        public decimal? TasaUsd { get; set; }
        public string? Fuente { get; set; }
        public bool? Activa { get; set; }
    }
}
