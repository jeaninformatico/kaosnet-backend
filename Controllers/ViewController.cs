using Api_Kaos_Net.DTO;
using Api_Kaos_Net.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Kaos_Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewController : ControllerBase
    {
        private readonly IViewService _service;

        public ViewController(IViewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ViewDto>> Create(ViewDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ViewId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ViewDto dto)
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
