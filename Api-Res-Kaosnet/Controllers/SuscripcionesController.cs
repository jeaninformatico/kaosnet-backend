using Api_Res_Kaosnet.DTO;
using Api_Res_Kaosnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Res_Kaosnet.Controllers
{
    [Route("api/suscripciones")]
    [ApiController]
    public class SuscripcionesController : ControllerBase
    {
        private readonly KaosnetOriginalContext _context;

        public SuscripcionesController(KaosnetOriginalContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<SuscripcioneDto>>> GetSuscripciones()
        {
            var suscripciones = await _context.Suscripciones
                .Select(s => new SuscripcioneDto
                {
                    IdSuscripcion = s.IdSuscripcion,
                    FechaInicio = s.FechaInicio,
                    FechaRenovacion = s.FechaRenovacion,
                    CicloPago = s.CicloPago,
                    PrecioUsd = s.PrecioUsd,
                    PrecioLocal = s.PrecioLocal,
                    Estado = s.Estado,
                    IdCuenta = s.IdCuenta,
                    IdCliente = s.IdCliente
                })
                .ToListAsync();

            return Ok(suscripciones);
        }

        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<SuscripcioneDto>> GetSuscripcion(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);
            if (suscripcion == null)
                return NotFound(new { mensaje = $"No se encontró ninguna suscripción con el ID {id}." });

            var dto = new SuscripcioneDto
            {
                IdSuscripcion = suscripcion.IdSuscripcion,
                FechaInicio = suscripcion.FechaInicio,
                FechaRenovacion = suscripcion.FechaRenovacion,
                CicloPago = suscripcion.CicloPago,
                PrecioUsd = suscripcion.PrecioUsd,
                PrecioLocal = suscripcion.PrecioLocal,
                Estado = suscripcion.Estado,
                IdCuenta = suscripcion.IdCuenta,
                IdCliente = suscripcion.IdCliente
            };

            return Ok(new { suscripcion = dto });
        }

        [HttpPost("crear")]
        public async Task<ActionResult<SuscripcioneDto>> CreateSuscripcion([FromBody] SuscripcioneCreateDto dto)
        {
            if (dto.IdCuenta == null || dto.IdCliente == null || dto.FechaInicio == null)
                return BadRequest(new { mensaje = "Cuenta, cliente y fecha de inicio son obligatorios." });

            var existe = await _context.Suscripciones
                .AnyAsync(s => s.IdCuenta == dto.IdCuenta && s.IdCliente == dto.IdCliente && s.FechaInicio == dto.FechaInicio);

            if (existe)
                return BadRequest(new { mensaje = "Ya existe una suscripción con esa cuenta, cliente y fecha de inicio." });

            var nuevaSuscripcion = new Suscripcione
            {
                FechaInicio = dto.FechaInicio,
                FechaRenovacion = dto.FechaRenovacion,
                CicloPago = dto.CicloPago,
                PrecioUsd = dto.PrecioUsd,
                PrecioLocal = dto.PrecioLocal,
                Estado = dto.Estado,
                IdCuenta = dto.IdCuenta,
                IdCliente = dto.IdCliente
            };

            _context.Suscripciones.Add(nuevaSuscripcion);
            await _context.SaveChangesAsync();

            var result = new SuscripcioneDto
            {
                IdSuscripcion = nuevaSuscripcion.IdSuscripcion,
                FechaInicio = nuevaSuscripcion.FechaInicio,
                FechaRenovacion = nuevaSuscripcion.FechaRenovacion,
                CicloPago = nuevaSuscripcion.CicloPago,
                PrecioUsd = nuevaSuscripcion.PrecioUsd,
                PrecioLocal = nuevaSuscripcion.PrecioLocal,
                Estado = nuevaSuscripcion.Estado,
                IdCuenta = nuevaSuscripcion.IdCuenta,
                IdCliente = nuevaSuscripcion.IdCliente
            };

            return CreatedAtAction(nameof(GetSuscripcion), new { id = nuevaSuscripcion.IdSuscripcion },
                new { mensaje = "Suscripción creada exitosamente.", suscripcion = result });
        }

        [HttpPut("editar")]
        public async Task<IActionResult> UpdateSuscripcion([FromBody] SuscripcioneUpdateDto dto)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(dto.IdSuscripcion);
            if (suscripcion == null)
                return NotFound(new { mensaje = $"No se encontró ninguna suscripción con el ID {dto.IdSuscripcion}." });

            var existe = await _context.Suscripciones
                .AnyAsync(s => s.IdCuenta == dto.IdCuenta && s.IdCliente == dto.IdCliente && s.FechaInicio == dto.FechaInicio && s.IdSuscripcion != dto.IdSuscripcion);

            if (existe)
                return BadRequest(new { mensaje = "Ya existe otra suscripción con esa cuenta, cliente y fecha de inicio." });

            suscripcion.FechaInicio = dto.FechaInicio;
            suscripcion.FechaRenovacion = dto.FechaRenovacion;
            suscripcion.CicloPago = dto.CicloPago;
            suscripcion.PrecioUsd = dto.PrecioUsd;
            suscripcion.PrecioLocal = dto.PrecioLocal;
            suscripcion.Estado = dto.Estado;
            suscripcion.IdCuenta = dto.IdCuenta;
            suscripcion.IdCliente = dto.IdCliente;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Suscripción actualizada correctamente." });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteSuscripcion(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);
            if (suscripcion == null)
                return NotFound(new { mensaje = $"No se encontró ninguna suscripción con el ID {id}." });

            _context.Suscripciones.Remove(suscripcion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Suscripción eliminada: {suscripcion.IdSuscripcion}" });
        }
    }
}
