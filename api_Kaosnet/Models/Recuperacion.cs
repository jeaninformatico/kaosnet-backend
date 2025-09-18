namespace api_Kaosnet.Models
{
    public class Recuperacion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
        public bool Usado { get; set; } = false;
        public DateTime CreadoEn { get; set; } = DateTime.Now;
    }

}
