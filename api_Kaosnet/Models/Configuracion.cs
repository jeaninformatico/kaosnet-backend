namespace api_Kaosnet.Models
{
    public class Configuracion
    {
        public int Id { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string? Valor { get; set; }
        public string? Tipo { get; set; }
        public DateTime ActualizadoEn { get; set; } = DateTime.Now;
    }

}
