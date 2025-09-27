using Api_Res_Kaosnet.DTO;
using Api_Res_Kaosnet.Helpers;
using Api_Res_Kaosnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Api_Res_Kaosnet.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly KaosnetOriginalContext _context;

        public UsuariosController(KaosnetOriginalContext context)
        {
            _context = context;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Correo) || string.IsNullOrWhiteSpace(dto.Clave))
                return BadRequest(new { mensaje = "Correo y clave son obligatorios." });

            var claveEncriptada = SeguridadHelper.EncriptarClave(dto.Clave);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo.ToLower() == dto.Correo.ToLower() && u.ClaveHash == claveEncriptada);

            if (usuario == null)
                return Unauthorized(new { mensaje = "Credenciales inválidas." });

            var result = new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                ImagenUrl = usuario.ImagenUrl,
                Telefono = usuario.Telefono,
                Cedula = usuario.Cedula,
                Activo = usuario.Activo,
                FechaRegistro = usuario.FechaRegistro,
                IdRol = usuario.IdRol
            };

            return Ok(new { mensaje = "Login exitoso.", usuario = result });
        }

        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDto
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    Correo = u.Correo,
                    ImagenUrl = u.ImagenUrl,
                    Telefono = u.Telefono,
                    Cedula = u.Cedula,
                    Activo = u.Activo,
                    FechaRegistro = u.FechaRegistro,
                    IdRol = u.IdRol
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró ningún usuario con el ID {id}." });

            var dto = new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                ImagenUrl = usuario.ImagenUrl,
                Telefono = usuario.Telefono,
                Cedula = usuario.Cedula,
                Activo = usuario.Activo,
                FechaRegistro = usuario.FechaRegistro,
                IdRol = usuario.IdRol
            };

            return Ok(new { usuario = dto });
        }

        [HttpPost("crear")]
        public async Task<ActionResult<UsuarioDto>> CreateUsuario([FromBody] UsuarioCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre) || string.IsNullOrWhiteSpace(dto.Correo))
                return BadRequest(new { mensaje = "Nombre y correo son obligatorios." });

            if (!Regex.IsMatch(dto.Telefono, @"^04(12|14|16|24|26)\d{7}$"))
                return BadRequest(new { mensaje = "El teléfono debe tener formato venezolano válido: 04167170715." });

            if (!Regex.IsMatch(dto.Cedula, @"^(V|E)?\d{6,9}$"))
                return BadRequest(new { mensaje = "La cédula debe ser venezolana válida: V12345678 o 12345678." });

            if (await _context.Usuarios.AnyAsync(u => u.Correo.ToLower() == dto.Correo.ToLower()))
                return BadRequest(new { mensaje = "Ya existe un usuario con ese correo." });

            if (await _context.Usuarios.AnyAsync(u => u.Cedula == dto.Cedula))
                return BadRequest(new { mensaje = "Ya existe un usuario con esa cédula." });

            if (await _context.Usuarios.AnyAsync(u => u.Telefono == dto.Telefono))
                return BadRequest(new { mensaje = "Ya existe un usuario con ese teléfono." });

            var claveEncriptada = SeguridadHelper.EncriptarClave(dto.ClaveHash);

            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                ClaveHash = claveEncriptada,
                ImagenUrl = dto.ImagenUrl,
                Telefono = dto.Telefono,
                Cedula = dto.Cedula,
                Activo = true,
                FechaRegistro = DateTime.Now,
                IdRol = dto.IdRol
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            var result = new UsuarioDto
            {
                IdUsuario = nuevoUsuario.IdUsuario,
                Nombre = nuevoUsuario.Nombre,
                Correo = nuevoUsuario.Correo,
                ImagenUrl = nuevoUsuario.ImagenUrl,
                Telefono = nuevoUsuario.Telefono,
                Cedula = nuevoUsuario.Cedula,
                Activo = nuevoUsuario.Activo,
                FechaRegistro = nuevoUsuario.FechaRegistro,
                IdRol = nuevoUsuario.IdRol
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = nuevoUsuario.IdUsuario },
                new { mensaje = $"Usuario creado exitosamente: {dto.Nombre}", usuario = result });
        }

        private string GuardarImagenDesdeBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return null;

            // Separar data:image/png;base64, si viene incluida
            var datos = base64.Contains(",") ? base64.Split(',')[1] : base64;

            var bytes = Convert.FromBase64String(datos);
            var nombreArchivo = $"{Guid.NewGuid()}.png"; // puedes cambiar extensión si quieres
            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            var rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);
            System.IO.File.WriteAllBytes(rutaArchivo, bytes);

            return $"{Request.Scheme}://{Request.Host}/imagenes/{nombreArchivo}";
        }


        [HttpPut("editar")]
        public async Task<IActionResult> UpdateUsuario([FromBody] UsuarioUpdateDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.IdUsuario);
            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró ningún usuario con el ID {dto.IdUsuario}." });

            if (!Regex.IsMatch(dto.Telefono, @"^04(12|14|16|24|26)\d{7}$"))
                return BadRequest(new { mensaje = "El teléfono debe tener formato venezolano válido: 04167170715." });

            if (!Regex.IsMatch(dto.Cedula, @"^(V|E)?\d{6,9}$"))
                return BadRequest(new { mensaje = "La cédula debe ser venezolana válida: V12345678 o 12345678." });

            if (await _context.Usuarios.AnyAsync(u => u.Correo.ToLower() == dto.Correo.ToLower() && u.IdUsuario != dto.IdUsuario))
                return BadRequest(new { mensaje = "Ya existe otro usuario con ese correo." });

            if (await _context.Usuarios.AnyAsync(u => u.Cedula == dto.Cedula && u.IdUsuario != dto.IdUsuario))
                return BadRequest(new { mensaje = "Ya existe otro usuario con esa cédula." });

            if (await _context.Usuarios.AnyAsync(u => u.Telefono == dto.Telefono && u.IdUsuario != dto.IdUsuario))
                return BadRequest(new { mensaje = "Ya existe otro usuario con ese teléfono." });

            usuario.Nombre = dto.Nombre;
            usuario.Correo = dto.Correo;
            usuario.ImagenUrl = dto.ImagenUrl;
            usuario.Telefono = dto.Telefono;
            usuario.Cedula = dto.Cedula;
            usuario.Activo = dto.Activo;
            usuario.IdRol = dto.IdRol;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Usuario actualizado correctamente: {dto.Nombre}" });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró ningún usuario con el ID {id}." });

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Usuario eliminado: {usuario.Nombre}" });
        }
    }
}
