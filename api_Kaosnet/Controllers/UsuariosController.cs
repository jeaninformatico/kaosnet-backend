using api_Kaosnet.Data;
using api_Kaosnet.Dtos;
using api_Kaosnet.Models;
using api_Kaosnet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api_Kaosnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _repo;

        public UsuariosController(AppDbContext context)
        {
            _repo = new UsuarioRepository(context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _repo.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _repo.GetByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] UsuarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var creado = await _repo.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
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
