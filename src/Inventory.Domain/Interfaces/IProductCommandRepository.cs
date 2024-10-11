using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface IProductCommandRepository
    {
        Task AddAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
        Task DeleteAsync(int id);
    }
}
