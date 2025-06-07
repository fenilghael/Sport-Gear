using System.Collections.Generic;
using System.Threading.Tasks;
using SportsGearCMS.DTOs;

namespace SportsGearCMS.Services
{
    public interface IManufacturerService
    {
        Task<IEnumerable<ManufacturerDto>> GetAllAsync();
        Task<ManufacturerDto> GetByIdAsync(int id);
        Task<ManufacturerDto> AddAsync(ManufacturerDto manufacturerDto);
        Task<ManufacturerDto> UpdateAsync(int id, ManufacturerDto manufacturerDto);
        Task<bool> DeleteAsync(int id);
    }
}
