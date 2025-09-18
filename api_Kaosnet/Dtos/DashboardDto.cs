namespace api_Kaosnet.Dtos
{
    public class DashboardDto
    {
        public int TotalUsuarios { get; set; }
        public int TotalCuentas { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TasaDolarActual { get; set; }
        public int RecuperacionesActivas { get; set; }
        public Dictionary<string, string?> Configuraciones { get; set; } = new();
    }
}
