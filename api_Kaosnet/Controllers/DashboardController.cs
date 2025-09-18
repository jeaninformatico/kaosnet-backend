using api_Kaosnet.Data;
using api_Kaosnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;

        public DashboardController(AppDbContext context)
        {
            _service = new DashboardService(context);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var datos = await _service.ObtenerAsync();
            return Ok(datos);
        }
    }
}
