using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracionController : ControllerBase
    {
        private readonly ConfiguracionRepository _repo;

        public ConfiguracionController(AppDbContext context)
        {
            _repo = new ConfiguracionRepository(context);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] ConfiguracionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var config = await _repo.CrearOActualizarAsync(dto);
            return Ok(new
            {
                config.Clave,
                config.Valor,
                config.Tipo,
                config.ActualizadoEn
            });
        }

        [HttpGet("{clave}")]
        public async Task<IActionResult> Obtener(string clave)
        {
            var config = await _repo.ObtenerPorClaveAsync(clave);
            if (config == null)
                return NotFound(new { error = "Configuración no encontrada." });

            return Ok(new
            {
                config.Clave,
                config.Valor,
                config.Tipo,
                config.ActualizadoEn
            });
        }

        [HttpGet("todas")]
        public async Task<IActionResult> Listar()
        {
            var lista = await _repo.ListarTodasAsync();
            return Ok(lista.Select(c => new
            {
                c.Clave,
                c.Valor,
                c.Tipo,
                c.ActualizadoEn
            }));
        }
    }
}
