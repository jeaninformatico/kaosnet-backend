// CuentaUpdateDto.cs
namespace Api_Res_Kaosnet.DTO
{
    public class CuentaUpdateDto
    {
        public int IdCuenta { get; set; }
        public string? CorreoAcceso { get; set; }
        public string? ClaveAcceso { get; set; }
        public string? Pin { get; set; }
        public string? TipoCuenta { get; set; }
        public int? Dispositivos { get; set; }
        public string? País { get; set; }
        public DateOnly? FechaExpira { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Estado { get; set; }
        public int? IdServicio { get; set; }
    }
}
