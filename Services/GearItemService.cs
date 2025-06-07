using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsGearCMS.Data;
using SportsGearCMS.DTOs;
using SportsGearCMS.Models;

namespace SportsGearCMS.Services
{
    public class GearItemService : IGearItemService
    {
        private readonly AppDbContext _context;

        public GearItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GearItemDto>> GetAllAsync()
        {
            return await _context.GearItems
                .Include(g => g.GearItemCategories).ThenInclude(gc => gc.Category)
                .Include(g => g.GearItemManufacturers).ThenInclude(gm => gm.Manufacturer)
                .Select(g => new GearItemDto
                {
                    GearItemId = g.GearItemId,
                    Name = g.Name,
                    Description = g.Description,
                    QuantityInStock = g.QuantityInStock,
                    Categories = g.GearItemCategories.Select(gc => gc.Category.Name).ToList(),
                    Manufacturers = g.GearItemManufacturers.Select(gm => gm.Manufacturer.Name).ToList()
                })
                .ToListAsync();
        }

        public async Task<GearItemDto> GetByIdAsync(int id)
        {
            var g = await _context.GearItems
                .Include(g => g.GearItemCategories).ThenInclude(gc => gc.Category)
                .Include(g => g.GearItemManufacturers).ThenInclude(gm => gm.Manufacturer)
                .FirstOrDefaultAsync(g => g.GearItemId == id);

            if (g == null) return null;

            return new GearItemDto
            {
                GearItemId = g.GearItemId,
                Name = g.Name,
                Description = g.Description,
                QuantityInStock = g.QuantityInStock,
                Categories = g.GearItemCategories.Select(gc => gc.Category.Name).ToList(),
                Manufacturers = g.GearItemManufacturers.Select(gm => gm.Manufacturer.Name).ToList()
            };
        }

        public async Task<GearItemDto> AddAsync(GearItemDto dto)
        {
            var gearItem = new GearItem
            {
                Name = dto.Name,
                Description = dto.Description,
                QuantityInStock = dto.QuantityInStock
            };

            _context.GearItems.Add(gearItem);
            await _context.SaveChangesAsync();

            dto.GearItemId = gearItem.GearItemId;
            return dto;
        }

        public async Task<GearItemDto> UpdateAsync(int id, GearItemDto dto)
        {
            var gearItem = await _context.GearItems.FindAsync(id);
            if (gearItem == null) return null;

            gearItem.Name = dto.Name;
            gearItem.Description = dto.Description;
            gearItem.QuantityInStock = dto.QuantityInStock;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gearItem = await _context.GearItems.FindAsync(id);
            if (gearItem == null) return false;

            _context.GearItems.Remove(gearItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
