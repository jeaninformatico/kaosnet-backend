using System.ComponentModel.DataAnnotations;

namespace api_Kaosnet.Dtos
{
    public class UsuarioDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string ClaveHash { get; set; } = string.Empty;

        public string? ImagenUrl { get; set; }

        [RegularExpression(@"^0(412|414|424|416|426)\d{7}$", ErrorMessage = "Teléfono venezolano inválido")]
        public string? Telefono { get; set; }

        [RegularExpression(@"^[VE]-\d{7,9}$", ErrorMessage = "Cédula venezolana inválida")]
        public string? Cedula { get; set; }

        public int? RolId { get; set; }
    }
}
