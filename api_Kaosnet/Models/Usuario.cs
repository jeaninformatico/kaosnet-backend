namespace api_Kaosnet.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string ClaveHash { get; set; } = string.Empty;
        public string? ImagenUrl { get; set; }
        public string? Telefono { get; set; }
        public string? Cedula { get; set; }
        public int? RolId { get; set; }
        public Rol? Rol { get; set; }
        public DateTime CreadoEn { get; set; } = DateTime.Now;
        public DateTime ActualizadoEn { get; set; } = DateTime.Now;
        public DateTime? EliminadoEn { get; set; }
    }
}
