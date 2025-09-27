using Api_Res_Kaosnet.DTO;
using Api_Res_Kaosnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Res_Kaosnet.Controllers
{
    [Route("api/servicios")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly KaosnetOriginalContext _context;

        public ServiciosController(KaosnetOriginalContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<ServicioDto>>> GetServicios()
        {
            var servicios = await _context.Servicios
                .Select(s => new ServicioDto
                {
                    IdServicio = s.IdServicio,
                    Nombre = s.Nombre,
                    Tipo = s.Tipo,
                    ImagenUrl = s.ImagenUrl,
                    PaísOrigen = s.PaísOrigen,
                    MonedaBae = s.MonedaBae,
                    Activo = s.Activo
                })
                .ToListAsync();

            return Ok(servicios);
        }

        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<ServicioDto>> GetServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
                return NotFound(new { mensaje = $"No se encontró ningún servicio con el ID {id}." });

            var dto = new ServicioDto
            {
                IdServicio = servicio.IdServicio,
                Nombre = servicio.Nombre,
                Tipo = servicio.Tipo,
                ImagenUrl = servicio.ImagenUrl,
                PaísOrigen = servicio.PaísOrigen,
                MonedaBae = servicio.MonedaBae,
                Activo = servicio.Activo
            };

            return Ok(new { servicio = dto });
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ServicioDto>> CreateServicio([FromBody] ServicioCreateEditDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest(new { mensaje = "El nombre del servicio es obligatorio." });

            var nombreExiste = await _context.Servicios
                .AnyAsync(s => s.Nombre.ToLower() == dto.Nombre.ToLower());

            if (nombreExiste)
                return BadRequest(new { mensaje = "Ya existe un servicio con ese nombre." });

            var nuevoServicio = new Servicio
            {
                Nombre = dto.Nombre,
                Tipo = dto.Tipo,
                ImagenUrl = dto.ImagenUrl,
                PaísOrigen = dto.PaísOrigen,
                MonedaBae = dto.MonedaBae,
                Activo = dto.Activo
            };

            _context.Servicios.Add(nuevoServicio);
            await _context.SaveChangesAsync();

            var result = new ServicioDto
            {
                IdServicio = nuevoServicio.IdServicio,
                Nombre = nuevoServicio.Nombre,
                Tipo = nuevoServicio.Tipo,
                ImagenUrl = nuevoServicio.ImagenUrl,
                PaísOrigen = nuevoServicio.PaísOrigen,
                MonedaBae = nuevoServicio.MonedaBae,
                Activo = nuevoServicio.Activo
            };

            return CreatedAtAction(nameof(GetServicio), new { id = nuevoServicio.IdServicio },
                new { mensaje = $"Servicio creado exitosamente: {dto.Nombre}", servicio = result });
        }

        [HttpPut("editar")]
        public async Task<IActionResult> UpdateServicio([FromBody] ServicioUpdateDto dto)
        {
            var servicio = await _context.Servicios.FindAsync(dto.IdServicio);
            if (servicio == null)
                return NotFound(new { mensaje = $"No se encontró ningún servicio con el ID {dto.IdServicio}." });

            var nombreExiste = await _context.Servicios
                .AnyAsync(s => s.Nombre.ToLower() == dto.Nombre.ToLower() && s.IdServicio != dto.IdServicio);

            if (nombreExiste)
                return BadRequest(new { mensaje = "Ya existe otro servicio con ese nombre." });

            servicio.Nombre = dto.Nombre;
            servicio.Tipo = dto.Tipo;
            servicio.ImagenUrl = dto.ImagenUrl;
            servicio.PaísOrigen = dto.PaísOrigen;
            servicio.MonedaBae = dto.MonedaBae;
            servicio.Activo = dto.Activo;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Servicio actualizado correctamente: {dto.Nombre}" });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
                return NotFound(new { mensaje = $"No se encontró ningún servicio con el ID {id}." });

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Servicio eliminado: {servicio.Nombre}" });
        }
    }
}
