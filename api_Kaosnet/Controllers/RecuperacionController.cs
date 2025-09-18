using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecuperacionController : ControllerBase
    {
        private readonly RecuperacionService _service;

        public RecuperacionController(AppDbContext context)
        {
            _service = new RecuperacionService(context);
        }

        [HttpPost("solicitar")]
        public async Task<IActionResult> Solicitar([FromBody] SolicitudRecuperacionDto dto)
        {
            try
            {
                var token = await _service.SolicitarAsync(dto.Correo);
                return Ok(new { token });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("confirmar")]
        public async Task<IActionResult> Confirmar([FromBody] ConfirmarRecuperacionDto dto)
        {
            try
            {
                var ok = await _service.ConfirmarAsync(dto.Token, dto.NuevaClave);
                return Ok(new { mensaje = "Contraseña actualizada correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
