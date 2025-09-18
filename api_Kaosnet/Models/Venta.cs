namespace api_Kaosnet.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        public Cuenta? Cuenta { get; set; }
        public decimal Monto { get; set; }
        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }

}
