using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsGearCMS.Data;
using SportsGearCMS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SportsGearCMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GearItemAssociationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GearItemAssociationsController(AppDbContext context)
        {
            _context = context;
        }

        // CATEGORY RELATIONSHIP METHODS

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategoryToGearItem(int gearItemId, int categoryId)
        {
            var exists = await _context.GearItemCategories
                .AnyAsync(x => x.GearItemId == gearItemId && x.CategoryId == categoryId);
            if (exists)
                return BadRequest("Category already associated.");

            _context.GearItemCategories.Add(new GearItemCategory
            {
                GearItemId = gearItemId,
                CategoryId = categoryId
            });
            await _context.SaveChangesAsync();

            return Ok("Category added.");
        }

        [HttpDelete("RemoveCategory")]
        public async Task<IActionResult> RemoveCategoryFromGearItem(int gearItemId, int categoryId)
        {
            var entity = await _context.GearItemCategories
                .FirstOrDefaultAsync(x => x.GearItemId == gearItemId && x.CategoryId == categoryId);
            if (entity == null) return NotFound();

            _context.GearItemCategories.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("CategoriesForGearItem/{gearItemId}")]
        public async Task<IActionResult> ListCategoriesForGearItem(int gearItemId)
        {
            var categories = await _context.GearItemCategories
                .Where(x => x.GearItemId == gearItemId)
                .Select(x => x.Category.Name)
                .ToListAsync();

            return Ok(categories);
        }

        // MANUFACTURER RELATIONSHIP METHODS

        [HttpPost("AddManufacturer")]
        public async Task<IActionResult> AddManufacturerToGearItem(int gearItemId, int manufacturerId)
        {
            var exists = await _context.GearItemManufacturers
                .AnyAsync(x => x.GearItemId == gearItemId && x.ManufacturerId == manufacturerId);
            if (exists)
                return BadRequest("Manufacturer already associated.");

            _context.GearItemManufacturers.Add(new GearItemManufacturer
            {
                GearItemId = gearItemId,
                ManufacturerId = manufacturerId
            });
            await _context.SaveChangesAsync();

            return Ok("Manufacturer added.");
        }

        [HttpDelete("RemoveManufacturer")]
        public async Task<IActionResult> RemoveManufacturerFromGearItem(int gearItemId, int manufacturerId)
        {
            var entity = await _context.GearItemManufacturers
                .FirstOrDefaultAsync(x => x.GearItemId == gearItemId && x.ManufacturerId == manufacturerId);
            if (entity == null) return NotFound();

            _context.GearItemManufacturers.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("ManufacturersForGearItem/{gearItemId}")]
        public async Task<IActionResult> ListManufacturersForGearItem(int gearItemId)
        {
            var manufacturers = await _context.GearItemManufacturers
                .Where(x => x.GearItemId == gearItemId)
                .Select(x => x.Manufacturer.Name)
                .ToListAsync();

            return Ok(manufacturers);
        }
    }
}
