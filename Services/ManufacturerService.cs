using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsGearCMS.Data;
using SportsGearCMS.DTOs;
using SportsGearCMS.Models;

namespace SportsGearCMS.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly AppDbContext _context;

        public ManufacturerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ManufacturerDto>> GetAllAsync()
        {
            return await _context.Manufacturers
                .Select(m => new ManufacturerDto
                {
                    ManufacturerId = m.ManufacturerId,
                    Name = m.Name,
                    ContactEmail = m.ContactEmail
                })
                .ToListAsync();
        }

        public async Task<ManufacturerDto> GetByIdAsync(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null) return null;

            return new ManufacturerDto
            {
                ManufacturerId = manufacturer.ManufacturerId,
                Name = manufacturer.Name,
                ContactEmail = manufacturer.ContactEmail
            };
        }

        public async Task<ManufacturerDto> AddAsync(ManufacturerDto dto)
        {
            var manufacturer = new Manufacturer
            {
                Name = dto.Name,
                ContactEmail = dto.ContactEmail
            };

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();

            dto.ManufacturerId = manufacturer.ManufacturerId;
            return dto;
        }

        public async Task<ManufacturerDto> UpdateAsync(int id, ManufacturerDto dto)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null) return null;

            manufacturer.Name = dto.Name;
            manufacturer.ContactEmail = dto.ContactEmail;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null) return false;

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
