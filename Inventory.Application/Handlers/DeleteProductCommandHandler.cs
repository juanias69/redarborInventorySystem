using Inventory.Application.Commands;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseResult>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(request.Id);
                if (existingProduct == null)
                    return new ResponseResult() { Success = false, Message = $"El producto con Id {request.Id} no existe." };
                await _productRepository.DeleteAsync(request.Id);

                return new ResponseResult() { Success = true, Message = "Registro eliminado correctamente" };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Message = ex.Message };
            }
        }
    }
}
