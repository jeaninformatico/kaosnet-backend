using System.Text.Json.Serialization;

namespace api_Kaosnet.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        [JsonIgnore]
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
