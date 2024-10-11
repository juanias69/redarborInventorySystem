using Inventory.Application.DTOs;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Queries.Products.Handler
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductQueryRepository _productRepository;

        public GetProductByIdQueryHandler(IProductQueryRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ProductDto product = new();
                var productResult = await _productRepository.GetByIdAsync(request.Id);
                if (productResult != null)
                {
                    product.Id = productResult.Id;
                    product.Name = productResult.Name;
                    product.Description = productResult.Description;
                    product.Price = productResult.Price;
                    product.CategoryId = productResult.CategoryId;
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("Error al obtener la informacion", ex.InnerException);
            }
        }
    }
}
