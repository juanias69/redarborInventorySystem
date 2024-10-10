using Inventory.Application.Commands;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;


namespace Inventory.Application.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ResponseResult>
    {
        private readonly IProductCommandRepository _productRepository;

        public AddProductCommandHandler(IProductCommandRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product
                {
                    Name = request.Product.Name,
                    Description = request.Product.Description,
                    CategoryId = request.Product.CategoryId,
                    Price = request.Product.Price
                };
                await _productRepository.AddAsync(product);

                return new ResponseResult() { Success = true, Message = "Registro insertado correctamente" };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Message = ex.Message };
            }
        }
    }
}
