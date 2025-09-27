using Api_Res_Kaosnet.DTO;
using Api_Res_Kaosnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Res_Kaosnet.Controllers
{
    [Route("api/tasacambio")]
    [ApiController]
    public class TasacambioController : ControllerBase
    {
        private readonly KaosnetOriginalContext _context;

        public TasacambioController(KaosnetOriginalContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<TasacambioDto>>> GetTasas()
        {
            var tasas = await _context.Tasacambios
                .Select(t => new TasacambioDto
                {
                    Idtasa = t.Idtasa,
                    Fecha = t.Fecha,
                    TasaUsd = t.TasaUsd,
                    Fuente = t.Fuente,
                    Activa = t.Activa
                })
                .ToListAsync();

            return Ok(tasas);
        }

        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<TasacambioDto>> GetTasa(int id)
        {
            var tasa = await _context.Tasacambios.FindAsync(id);
            if (tasa == null)
                return NotFound(new { mensaje = $"No se encontró ninguna tasa con el ID {id}." });

            var dto = new TasacambioDto
            {
                Idtasa = tasa.Idtasa,
                Fecha = tasa.Fecha,
                TasaUsd = tasa.TasaUsd,
                Fuente = tasa.Fuente,
                Activa = tasa.Activa
            };

            return Ok(new { tasa = dto });
        }

        [HttpPost("crear")]
        public async Task<ActionResult<TasacambioDto>> CreateTasa([FromBody] TasacambioCreateDto dto)
        {
            if (dto.Fecha == null || dto.TasaUsd == null)
                return BadRequest(new { mensaje = "La fecha y la tasa son obligatorias." });

            var existe = await _context.Tasacambios
                .AnyAsync(t => t.Fecha == dto.Fecha && t.Fuente == dto.Fuente);

            if (existe)
                return BadRequest(new { mensaje = "Ya existe una tasa registrada para esa fecha y fuente." });

            var nuevaTasa = new Tasacambio
            {
                Fecha = dto.Fecha,
                TasaUsd = dto.TasaUsd,
                Fuente = dto.Fuente,
                Activa = dto.Activa
            };

            _context.Tasacambios.Add(nuevaTasa);
            await _context.SaveChangesAsync();

            var result = new TasacambioDto
            {
                Idtasa = nuevaTasa.Idtasa,
                Fecha = nuevaTasa.Fecha,
                TasaUsd = nuevaTasa.TasaUsd,
                Fuente = nuevaTasa.Fuente,
                Activa = nuevaTasa.Activa
            };

            return CreatedAtAction(nameof(GetTasa), new { id = nuevaTasa.Idtasa },
                new { mensaje = "Tasa registrada exitosamente.", tasa = result });
        }

        [HttpPut("editar")]
        public async Task<IActionResult> UpdateTasa([FromBody] TasacambioUpdateDto dto)
        {
            var tasa = await _context.Tasacambios.FindAsync(dto.Idtasa);
            if (tasa == null)
                return NotFound(new { mensaje = $"No se encontró ninguna tasa con el ID {dto.Idtasa}." });

            var existe = await _context.Tasacambios
                .AnyAsync(t => t.Fecha == dto.Fecha && t.Fuente == dto.Fuente && t.Idtasa != dto.Idtasa);

            if (existe)
                return BadRequest(new { mensaje = "Ya existe otra tasa con esa fecha y fuente." });

            tasa.Fecha = dto.Fecha;
            tasa.TasaUsd = dto.TasaUsd;
            tasa.Fuente = dto.Fuente;
            tasa.Activa = dto.Activa;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Tasa actualizada correctamente." });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteTasa(int id)
        {
            var tasa = await _context.Tasacambios.FindAsync(id);
            if (tasa == null)
                return NotFound(new { mensaje = $"No se encontró ninguna tasa con el ID {id}." });

            _context.Tasacambios.Remove(tasa);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Tasa eliminada: {tasa.Fecha} - {tasa.Fuente}" });
        }
    }
}
