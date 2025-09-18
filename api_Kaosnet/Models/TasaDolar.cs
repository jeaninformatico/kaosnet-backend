namespace api_Kaosnet.Models
{
    public class TasaDolar
    {
        public int Id { get; set; }
        public decimal Tasa { get; set; }
        public string? Fuente { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

}
