namespace api_Kaosnet.Dtos
{
    public class CuentaDto
    {
        public int UsuarioId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Saldo { get; set; } = 0;
    }
}
