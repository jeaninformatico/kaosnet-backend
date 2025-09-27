namespace Api_Res_Kaosnet.DTO
{
    public class ServicioCreateEditDto
    {
        public string Nombre { get; set; } = null!;
        public string? Tipo { get; set; }
        public string? ImagenUrl { get; set; }
        public string? PaísOrigen { get; set; }
        public string? MonedaBae { get; set; }
        public bool? Activo { get; set; }
    }
}
