using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface ICategoryCommandRepository
    {
        Task AddAsync(CategoryModel category);
        Task DeleteAsync(int id);
    }
}
