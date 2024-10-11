
using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface IInventoryCommandRepository
    {
        Task AddAsync(InventoryModel inventory);
        Task UpdateAsync(InventoryModel inventory);
    }
}
