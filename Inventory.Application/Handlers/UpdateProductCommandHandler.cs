using Inventory.Application.Commands;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseResult>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(request.Id);
                if (existingProduct == null)
                    return new ResponseResult() { Success = false, Message = $"El producto con Id {request.Id} no existe." };
                var product = new Product
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    CategoryId = request.CategoryId,
                    Price = request.Price
                };
                await _productRepository.UpdateAsync(product);

                return new ResponseResult() { Success = true, Message = "Registro actualizado correctamente" };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Message = ex.Message };
            }
        }
    }
}
