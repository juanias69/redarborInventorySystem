using Inventory.Application.Commands.Products;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Commands.Inventory.Handlres
{
    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, ResponseResult>
    {
        private readonly IInventoryCommandRepository _inventoryRepository;

        public UpdateInventoryCommandHandler(IInventoryCommandRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ResponseResult> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var inventory = new InventoryModel
                {
                    Id = (int)request.Inventory.Id!,
                    ProductId = request.Inventory.ProductId,
                    Quantity = request.Inventory.Quantity,
                    EntryDate = request.Inventory.EntryDate
                };
                await _inventoryRepository.UpdateAsync(inventory);

                return new ResponseResult() { Success = true, Message = "Registro actualizado correctamente" };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Message = ex.Message };
            }
        }
    }
}
