using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface IProductCommandRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
