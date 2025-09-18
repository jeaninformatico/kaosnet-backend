using System.ComponentModel.DataAnnotations;

namespace api_Kaosnet.Dtos
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Clave { get; set; } = string.Empty;
    }
}
