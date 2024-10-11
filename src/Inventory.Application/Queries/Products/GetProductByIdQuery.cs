using Inventory.Application.DTOs;
using MediatR;

namespace Inventory.Application.Queries.Products
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}
