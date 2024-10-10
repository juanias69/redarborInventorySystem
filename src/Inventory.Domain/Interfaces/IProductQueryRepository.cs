using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface IProductQueryRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
