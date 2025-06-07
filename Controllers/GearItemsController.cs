using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SportsGearCMS.Services;
using SportsGearCMS.DTOs;

namespace SportsGearCMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GearItemsController : ControllerBase
    {
        private readonly IGearItemService _gearItemService;

        public GearItemsController(IGearItemService gearItemService)
        {
            _gearItemService = gearItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _gearItemService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _gearItemService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] GearItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var added = await _gearItemService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = added.GearItemId }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GearItemDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _gearItemService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _gearItemService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
