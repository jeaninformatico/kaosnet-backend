// SuscripcioneCreateDto.cs
namespace Api_Res_Kaosnet.DTO
{
    public class SuscripcioneCreateDto
    {
        public DateOnly? FechaInicio { get; set; }
        public DateOnly? FechaRenovacion { get; set; }
        public string? CicloPago { get; set; }
        public decimal? PrecioUsd { get; set; }
        public decimal? PrecioLocal { get; set; }
        public string? Estado { get; set; }
        public int? IdCuenta { get; set; }
        public int? IdCliente { get; set; }
    }
}
