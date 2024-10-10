using Inventory.Application.Commands;
using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.IServices
{
    public interface IProductAppService
    {
        Task<ResponseResult> AddProductAsync(ProductDto productDto);
        Task<ResponseResult> UpdateProductAsync(ProductDto productDto);
        Task<ResponseResult> DeleteProductAsync(int id);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }

}
