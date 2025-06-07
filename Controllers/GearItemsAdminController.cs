using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsGearCMS.Data;
using SportsGearCMS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SportsGearCMS.Controllers
{
    [Route("[controller]/[action]")]
    public class GearItemsAdminController : Controller
    {
        private readonly AppDbContext _context;

        public GearItemsAdminController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.GearItems.ToListAsync();
            return View(items);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GearItem item)
        {
            if (!ModelState.IsValid) return View(item);

            _context.GearItems.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.GearItems.FindAsync(id);
            return item == null ? NotFound() : View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GearItem item)
        {
            if (id != item.GearItemId) return NotFound();

            if (!ModelState.IsValid) return View(item);

            _context.Update(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.GearItems.FindAsync(id);
            return item == null ? NotFound() : View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.GearItems.FindAsync(id);
            if (item != null)
            {
                _context.GearItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.GearItems.FindAsync(id);
            return item == null ? NotFound() : View(item);
        }

        // GET: Assign Categories
        public async Task<IActionResult> AssignCategories(int id)
        {
            var gearItem = await _context.GearItems
                .Include(g => g.GearItemCategories)
                .FirstOrDefaultAsync(g => g.GearItemId == id);
            if (gearItem == null) return NotFound();

            var allCategories = await _context.Categories.ToListAsync();
            var assigned = gearItem.GearItemCategories.Select(gc => gc.CategoryId).ToHashSet();

            ViewBag.Categories = allCategories;
            ViewBag.Assigned = assigned;
            ViewBag.GearItemId = id;
            ViewBag.GearItemName = gearItem.Name;

            return View("AssignCategories");
        }

        [HttpPost]
        public async Task<IActionResult> AssignCategories(int id, List<int> selectedCategories)
        {
            var gearItem = await _context.GearItems
                .Include(g => g.GearItemCategories)
                .FirstOrDefaultAsync(g => g.GearItemId == id);
            if (gearItem == null) return NotFound();

            // Clear current assignments
            _context.GearItemCategories.RemoveRange(gearItem.GearItemCategories);

            // Add selected
            foreach (var categoryId in selectedCategories)
            {
                _context.GearItemCategories.Add(new GearItemCategory
                {
                    GearItemId = id,
                    CategoryId = categoryId
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Assign Manufacturers
        public async Task<IActionResult> AssignManufacturers(int id)
        {
            var gearItem = await _context.GearItems
                .Include(g => g.GearItemManufacturers)
                .FirstOrDefaultAsync(g => g.GearItemId == id);
            if (gearItem == null) return NotFound();

            var allManufacturers = await _context.Manufacturers.ToListAsync();
            var assigned = gearItem.GearItemManufacturers.Select(gm => gm.ManufacturerId).ToHashSet();

            ViewBag.Manufacturers = allManufacturers;
            ViewBag.Assigned = assigned;
            ViewBag.GearItemId = id;
            ViewBag.GearItemName = gearItem.Name;

            return View("AssignManufacturers");
        }

        [HttpPost]
        public async Task<IActionResult> AssignManufacturers(int id, List<int> selectedManufacturers)
        {
            var gearItem = await _context.GearItems
                .Include(g => g.GearItemManufacturers)
                .FirstOrDefaultAsync(g => g.GearItemId == id);
            if (gearItem == null) return NotFound();

            // Clear current assignments
            _context.GearItemManufacturers.RemoveRange(gearItem.GearItemManufacturers);

            // Add selected
            foreach (var manufacturerId in selectedManufacturers)
            {
                _context.GearItemManufacturers.Add(new GearItemManufacturer
                {
                    GearItemId = id,
                    ManufacturerId = manufacturerId
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}
