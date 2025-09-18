using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly CuentaRepository _repo;

        public CuentasController(AppDbContext context)
        {
            _repo = new CuentaRepository(context);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CuentaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cuenta = await _repo.CreateAsync(dto);
            return Ok(new
            {
                cuenta.Id,
                cuenta.Tipo,
                cuenta.Saldo,
                cuenta.Estado,
                cuenta.CreadoEn,
                Usuario = new
                {
                    cuenta.Usuario?.Id,
                    cuenta.Usuario?.Nombre,
                    cuenta.Usuario?.Correo
                }
            });
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerPorUsuario(int usuarioId)
        {
            var cuentas = await _repo.GetByUsuarioAsync(usuarioId);
            return Ok(cuentas.Select(c => new
            {
                c.Id,
                c.Tipo,
                c.Saldo,
                c.Estado,
                c.CreadoEn
            }));
        }
    }
}
