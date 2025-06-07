using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SportsGearCMS.Services;
using SportsGearCMS.DTOs;

namespace SportsGearCMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturersController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var manufacturers = await _manufacturerService.GetAllAsync();
            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var manufacturer = await _manufacturerService.GetByIdAsync(id);
            if (manufacturer == null)
                return NotFound();

            return Ok(manufacturer);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ManufacturerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var added = await _manufacturerService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = added.ManufacturerId }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ManufacturerDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _manufacturerService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _manufacturerService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
