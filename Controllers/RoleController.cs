using Api_Kaos_Net.DTO;
using Api_Kaos_Net.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Kaos_Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(int id)
        {
            var role = await _service.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(RoleDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.RoleId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoleDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
