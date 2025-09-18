using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AppDbContext context)
        {
            _auth = new AuthService(context);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _auth.LoginAsync(dto);
            if (usuario == null)
                return Unauthorized("Credenciales inválidas.");

            return Ok(new
            {
                usuario.Id,
                usuario.Nombre,
                usuario.Correo,
                usuario.ImagenUrl,
                usuario.Telefono,
                usuario.Cedula,
                Rol = usuario.Rol?.Nombre
            });
        }
    }
}
