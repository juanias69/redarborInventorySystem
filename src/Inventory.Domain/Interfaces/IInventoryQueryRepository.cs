
using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface IInventoryQueryRepository
    {
        Task<InventoryModel> GetByIdAsync(int id);
        Task<IEnumerable<InventoryModel>> GetAllAsync();
    }
}
