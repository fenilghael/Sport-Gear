using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SportsGearCMS.Services;
using SportsGearCMS.DTOs;

namespace SportsGearCMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var added = await _categoryService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = added.CategoryId }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _categoryService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _categoryService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
