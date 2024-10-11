using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;


namespace Inventory.Application.Commands.Inventory.Handlres
{
    public class AddInventoryCommandHandler : IRequestHandler<AddInventoryCommand, ResponseResult>
    {
        private readonly IInventoryCommandRepository _inventoryRepository;

        public AddInventoryCommandHandler(IInventoryCommandRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<ResponseResult> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new InventoryModel
                {
                    ProductId = request.inventory.ProductId,
                    Quantity = request.inventory.Quantity,
                    EntryDate = request.inventory.EntryDate
                };
                await _inventoryRepository.AddAsync(product);

                return new ResponseResult() { Success = true, Message = "Registro insertado correctamente" };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Message = ex.Message };
            }
        }
    }
}
