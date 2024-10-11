using Inventory.Application.DTOs;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IInventoryAppService
    {
        Task<ResponseResult> AddInventoryAsync(InventoryDto inventoryDto);
        Task<ResponseResult> UpdateInventoryAsync(InventoryDto inventoryDto);
        Task<InventoryDto> GetInventoryByProductIdAsync(int id);
        Task<IEnumerable<InventoryDto>> GetAllInventoriesAsync();
    }
}
