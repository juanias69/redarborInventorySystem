using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces
{
    public interface IProductQueryRepository
    {
        Task<ProductModel> GetByIdAsync(int id);
        Task<IEnumerable<ProductModel>> GetAllAsync();
    }
}
