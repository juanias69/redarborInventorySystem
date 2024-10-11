using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface ICategoryAppService
    {
        Task<ResponseResult> AddCategoryAsync(CategoryDto categoryDto);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<ResponseResult> DeleteCategoryAsync(int id);
    }
}
