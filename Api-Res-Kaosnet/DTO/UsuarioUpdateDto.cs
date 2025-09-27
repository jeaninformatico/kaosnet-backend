// UsuarioUpdateDto.cs
namespace Api_Res_Kaosnet.DTO
{
    public class UsuarioUpdateDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? ImagenUrl { get; set; }
        public string Telefono { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public bool? Activo { get; set; }
        public int? IdRol { get; set; }
    }
}
