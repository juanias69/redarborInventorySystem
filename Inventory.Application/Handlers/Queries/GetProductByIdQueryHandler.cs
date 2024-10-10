using Inventory.Application.Queries;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductQueryRepository _productRepository;

        public GetProductByIdQueryHandler(IProductQueryRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _productRepository.GetByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("Error al obtener la informacion", ex.InnerException);
            }
        }
    }
}
