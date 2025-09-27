// UsuarioCreateDto.cs
namespace Api_Res_Kaosnet.DTO
{
    public class UsuarioCreateDto
    {
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string ClaveHash { get; set; } = null!;
        public string? ImagenUrl { get; set; }
        public string Telefono { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public int? IdRol { get; set; }
    }
}
