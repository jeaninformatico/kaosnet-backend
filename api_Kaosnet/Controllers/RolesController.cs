using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;
using api_Kaosnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RolRepository _repo;

        public RolesController(AppDbContext context)
        {
            _repo = new RolRepository(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetAll()
        {
            var roles = await _repo.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetById(int id)
        {
            var rol = await _repo.GetByIdAsync(id);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> Create([FromBody] RolDto dto)
        {
            var nuevoRol = new Rol { Nombre = dto.Nombre, Descripcion = dto.Descripcion };
            var creado = await _repo.CreateAsync(nuevoRol);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolDto dto)
        {
            var actualizado = await _repo.UpdateAsync(id, dto);
            if (!actualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _repo.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
