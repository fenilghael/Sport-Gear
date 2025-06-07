using System.Collections.Generic;
using System.Threading.Tasks;
using SportsGearCMS.DTOs;

namespace SportsGearCMS.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> AddAsync(CategoryDto categoryDto);
        Task<CategoryDto> UpdateAsync(int id, CategoryDto categoryDto);
        Task<bool> DeleteAsync(int id);
    }
}
