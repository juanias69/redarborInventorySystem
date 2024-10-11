using Inventory.Application.DTOs;
using MediatR;

namespace Inventory.Application.Queries.Products
{
    public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
}
