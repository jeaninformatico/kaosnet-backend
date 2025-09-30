using Api_Kaos_Net.DTO;
using Api_Kaos_Net.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Kaos_Net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlanAccountController : ControllerBase
    {
        private readonly ISubscriptionPlanAccountService _service;

        public SubscriptionPlanAccountController(ISubscriptionPlanAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionPlanAccountDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionPlanAccountDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionPlanAccountDto>> Create(SubscriptionPlanAccountDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SubscriptionPlanAccountId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SubscriptionPlanAccountDto dto)
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
