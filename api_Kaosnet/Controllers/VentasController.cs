using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly VentaRepository _repo;

        public VentasController(AppDbContext context)
        {
            _repo = new VentaRepository(context);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] VentaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var venta = await _repo.CreateAsync(dto);
                return Ok(new
                {
                    venta.Id,
                    venta.Monto,
                    venta.Descripcion,
                    venta.Fecha,
                    Cuenta = new
                    {
                        venta.Cuenta?.Id,
                        venta.Cuenta?.Tipo,
                        venta.Cuenta?.Saldo
                    }
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("cuenta/{cuentaId}")]
        public async Task<IActionResult> ObtenerPorCuenta(int cuentaId)
        {
            var ventas = await _repo.GetByCuentaAsync(cuentaId);
            return Ok(ventas.Select(v => new
            {
                v.Id,
                v.Monto,
                v.Descripcion,
                v.Fecha
            }));
        }
    }
}
