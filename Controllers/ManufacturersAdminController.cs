using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsGearCMS.Data;
using SportsGearCMS.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SportsGearCMS.Controllers
{
    [Route("[controller]/[action]")]
    public class ManufacturersAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ManufacturersAdminController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Manufacturers.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid) return View(manufacturer);

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            return manufacturer == null ? NotFound() : View(manufacturer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Manufacturer manufacturer)
        {
            if (id != manufacturer.ManufacturerId) return NotFound();
            if (!ModelState.IsValid) return View(manufacturer);

            _context.Update(manufacturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            return manufacturer == null ? NotFound() : View(manufacturer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                _context.Manufacturers.Remove(manufacturer);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            return manufacturer == null ? NotFound() : View(manufacturer);
        }
    }
}
