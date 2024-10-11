using Inventory.Application.DTOs;
using Inventory.Application.Queries.Products;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Queries.Inventories.Handler
{
    public class GetInventaryByProductIdQueryHandler : IRequestHandler<GetInventarytByProductIdQuery, InventoryDto>
    {
        private readonly IInventoryQueryRepository _inventoryQueryRepository;

        public GetInventaryByProductIdQueryHandler(IInventoryQueryRepository inventoryQueryRepository)
        {
            _inventoryQueryRepository = inventoryQueryRepository;
        }

        public async Task<InventoryDto> Handle(GetInventarytByProductIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                InventoryDto inventory = new();
                var inventoryResult = await _inventoryQueryRepository.GetByIdAsync(request.Id);
                if (inventoryResult != null)
                {
                    inventory.Id = inventoryResult.Id;
                    inventory.ProductId = inventoryResult.ProductId;
                    inventory.Quantity = inventoryResult.Quantity;
                    inventory.EntryDate = inventoryResult.EntryDate;
                }

                return inventory;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("Error al obtener la informacion", ex.InnerException);
            }
        }
    }
}
