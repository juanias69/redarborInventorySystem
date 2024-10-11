using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Commands.Products.Handlres
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseResult>
    {
        private readonly IProductCommandRepository _productRepository;

        public UpdateProductCommandHandler(IProductCommandRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new ProductModel
                {
                    Id = (int)request.Product.Id!,
                    Name = request.Product.Name,
                    Description = request.Product.Description,
                    CategoryId = request.Product.CategoryId,
                    Price = request.Product.Price
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
