
using Inventory.Application.DTOs;
using Inventory.Application.Queries.Products;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Queries.Inventories.Handler
{
    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoriesQuery, IEnumerable<InventoryDto>>
    {
        private readonly IInventoryQueryRepository _inventoryQueryRepository;

        public GetAllInventoryQueryHandler(IInventoryQueryRepository inventoryQueryRepository)
        {
            _inventoryQueryRepository = inventoryQueryRepository;
        }

        public async Task<IEnumerable<InventoryDto>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<InventoryDto> ltInventories = new();
                var inventories = await _inventoryQueryRepository.GetAllAsync();
                if (ltInventories != null)
                {
                    ltInventories = inventories.Select(p => new InventoryDto
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        Quantity = p.Quantity,
                        EntryDate = p.EntryDate
                    }).ToList();
                }

                return ltInventories;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("Error al obtener la informacion", ex.InnerException);
            }
        }
    }
}
