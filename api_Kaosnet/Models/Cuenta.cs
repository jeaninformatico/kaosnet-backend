namespace api_Kaosnet.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Saldo { get; set; } = 0;
        public string Estado { get; set; } = "activa";
        public DateTime CreadoEn { get; set; } = DateTime.Now;
    }

}
