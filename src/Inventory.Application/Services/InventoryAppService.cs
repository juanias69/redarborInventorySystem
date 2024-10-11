using Inventory.Application.Commands.Inventory;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Queries.Inventories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Services
{
    public class InventoryAppService : IInventoryAppService 
    {
        private readonly IMediator _mediator;

        public InventoryAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseResult> AddInventoryAsync(InventoryDto inventoryDto)
        {
            var command = new AddInventoryCommand(inventoryDto);
            return await _mediator.Send(command);
        }

        public async Task<ResponseResult> UpdateInventoryAsync(InventoryDto inventoryDto)
        {
            var command = new UpdateInventoryCommand(inventoryDto);
            return await _mediator.Send(command);
        }

        public async Task<InventoryDto> GetInventoryByProductIdAsync(int id)
        {
            var query = new GetInventarytByProductIdQuery(id);
            return await _mediator.Send(query);
        }

        public async Task<IEnumerable<InventoryDto>> GetAllInventoriesAsync()
        {
            var query = new GetAllInventoriesQuery();
            return await _mediator.Send(query);
        }
    }
}
