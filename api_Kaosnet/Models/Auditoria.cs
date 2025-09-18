namespace api_Kaosnet.Models
{
    public class Auditoria
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string? Detalle { get; set; }
        public string? Ip { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

}
