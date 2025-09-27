using Api_Res_Kaosnet.DTO;
using Api_Res_Kaosnet.Helpers;
using Api_Res_Kaosnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Res_Kaosnet.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly KaosnetOriginalContext _context;

        public CuentasController(KaosnetOriginalContext context)
        {
            _context = context;
        }

        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<CuentaDto>>> GetCuentas()
        {
            var cuentas = await _context.Cuentas
                .Select(c => new CuentaDto
                {
                    IdCuenta = c.IdCuenta,
                    CorreoAcceso = c.CorreoAcceso,
                    TipoCuenta = c.TipoCuenta,
                    Dispositivos = c.Dispositivos,
                    País = c.País,
                    FechaExpira = c.FechaExpira,
                    ImagenUrl = c.ImagenUrl,
                    Estado = c.Estado,
                    IdServicio = c.IdServicio
                })
                .ToListAsync();

            return Ok(cuentas);
        }

        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<CuentaDto>> GetCuenta(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
                return NotFound(new { mensaje = $"No se encontró ninguna cuenta con el ID {id}." });

            var dto = new CuentaDto
            {
                IdCuenta = cuenta.IdCuenta,
                CorreoAcceso = cuenta.CorreoAcceso,
                TipoCuenta = cuenta.TipoCuenta,
                Dispositivos = cuenta.Dispositivos,
                País = cuenta.País,
                FechaExpira = cuenta.FechaExpira,
                ImagenUrl = cuenta.ImagenUrl,
                Estado = cuenta.Estado,
                IdServicio = cuenta.IdServicio
            };

            return Ok(new { cuenta = dto });
        }

        [HttpPost("crear")]
        public async Task<ActionResult<CuentaDto>> CreateCuenta([FromBody] CuentaCreateDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.CorreoAcceso))
            {
                var correoExiste = await _context.Cuentas
                    .AnyAsync(c => c.CorreoAcceso != null && c.CorreoAcceso.ToLower() == dto.CorreoAcceso.ToLower());

                if (correoExiste)
                    return BadRequest(new { mensaje = "Ya existe una cuenta con ese correo de acceso." });
            }

            var nuevaCuenta = new Cuenta
            {
                CorreoAcceso = dto.CorreoAcceso,
                ClaveAcceso = SeguridadHelper.EncriptarClave(dto.ClaveAcceso),
                Pin = SeguridadHelper.EncriptarClave(dto.Pin),
                TipoCuenta = dto.TipoCuenta,
                Dispositivos = dto.Dispositivos,
                País = dto.País,
                FechaExpira = dto.FechaExpira,
                ImagenUrl = dto.ImagenUrl,
                Estado = dto.Estado,
                IdServicio = dto.IdServicio
            };

            _context.Cuentas.Add(nuevaCuenta);
            await _context.SaveChangesAsync();

            var result = new CuentaDto
            {
                IdCuenta = nuevaCuenta.IdCuenta,
                CorreoAcceso = nuevaCuenta.CorreoAcceso,
                TipoCuenta = nuevaCuenta.TipoCuenta,
                Dispositivos = nuevaCuenta.Dispositivos,
                País = nuevaCuenta.País,
                FechaExpira = nuevaCuenta.FechaExpira,
                ImagenUrl = nuevaCuenta.ImagenUrl,
                Estado = nuevaCuenta.Estado,
                IdServicio = nuevaCuenta.IdServicio
            };

            return CreatedAtAction(nameof(GetCuenta), new { id = nuevaCuenta.IdCuenta },
                new { mensaje = $"Cuenta creada exitosamente.", cuenta = result });
        }

        [HttpPut("editar")]
        public async Task<IActionResult> UpdateCuenta([FromBody] CuentaUpdateDto dto)
        {
            var cuenta = await _context.Cuentas.FindAsync(dto.IdCuenta);
            if (cuenta == null)
                return NotFound(new { mensaje = $"No se encontró ninguna cuenta con el ID {dto.IdCuenta}." });

            if (!string.IsNullOrWhiteSpace(dto.CorreoAcceso))
            {
                var correoExiste = await _context.Cuentas
                    .AnyAsync(c => c.CorreoAcceso != null && c.CorreoAcceso.ToLower() == dto.CorreoAcceso.ToLower() && c.IdCuenta != dto.IdCuenta);

                if (correoExiste)
                    return BadRequest(new { mensaje = "Ya existe otra cuenta con ese correo de acceso." });
            }

            cuenta.CorreoAcceso = dto.CorreoAcceso;
            cuenta.ClaveAcceso = SeguridadHelper.EncriptarClave(dto.ClaveAcceso);
            cuenta.Pin = SeguridadHelper.EncriptarClave(dto.Pin);
            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.Dispositivos = dto.Dispositivos;
            cuenta.País = dto.País;
            cuenta.FechaExpira = dto.FechaExpira;
            cuenta.ImagenUrl = dto.ImagenUrl;
            cuenta.Estado = dto.Estado;
            cuenta.IdServicio = dto.IdServicio;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Cuenta actualizada correctamente." });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
                return NotFound(new { mensaje = $"No se encontró ninguna cuenta con el ID {id}." });

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Cuenta eliminada: {cuenta.CorreoAcceso}" });
        }
    }
}
