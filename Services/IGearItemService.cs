using System.Collections.Generic;
using System.Threading.Tasks;
using SportsGearCMS.DTOs;

namespace SportsGearCMS.Services
{
    public interface IGearItemService
    {
        Task<IEnumerable<GearItemDto>> GetAllAsync();
        Task<GearItemDto> GetByIdAsync(int id);
        Task<GearItemDto> AddAsync(GearItemDto gearItemDto);
        Task<GearItemDto> UpdateAsync(int id, GearItemDto gearItemDto);
        Task<bool> DeleteAsync(int id);
    }
}
