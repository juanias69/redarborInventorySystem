using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Queries.Products.Handler
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductQueryRepository _productRepository;

        public GetAllProductsQueryHandler(IProductQueryRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<ProductDto> ltProductos = new();
                var productos = await _productRepository.GetAllAsync();
                if (productos != null)
                {
                    ltProductos = productos.Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        CategoryId = p.CategoryId
                    }).ToList();
                }

                return ltProductos;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("Error al obtener la informacion", ex.InnerException);
            }
        }
    }
}
