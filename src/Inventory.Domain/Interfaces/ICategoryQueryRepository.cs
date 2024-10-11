using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface ICategoryQueryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllAsync();
    }
}
