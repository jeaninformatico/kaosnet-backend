using api_Kaosnet.Data;
using api_Kaosnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasaController : ControllerBase
    {
        private readonly TasaDolarService _service;

        public TasaController(AppDbContext context)
        {
            _service = new TasaDolarService(context);
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> Actualizar()
        {
            try
            {
                var tasa = await _service.ActualizarDesdeDolarApiAsync();
                return Ok(new
                {
                    tasa.Tasa,
                    tasa.Fuente,
                    tasa.Fecha
                });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, new { error = ex.Message });
            }
        }

        [HttpGet("ultima")]
        public async Task<IActionResult> ObtenerUltima()
        {
            var tasa = await _service.ObtenerUltimaAsync();
            if (tasa == null)
                return NotFound(new { error = "No hay tasas registradas." });

            return Ok(new
            {
                tasa.Tasa,
                tasa.Fuente,
                tasa.Fecha
            });
        }
    }
}
