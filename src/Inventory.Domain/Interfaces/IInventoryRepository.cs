using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventorys> GetByProductIdAsync(int productId);
    }

}
