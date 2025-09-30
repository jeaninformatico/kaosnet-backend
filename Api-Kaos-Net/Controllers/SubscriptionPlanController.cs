using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Kaos_Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly ISubscriptionPlanService _service;

        public SubscriptionPlanController(ISubscriptionPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionPlanDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionPlanDto>> GetById(int id)
        {
            var plan = await _service.GetByIdAsync(id);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionPlanDto>> Create(SubscriptionPlanDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SubscriptionPlanId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SubscriptionPlanDto dto)
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
