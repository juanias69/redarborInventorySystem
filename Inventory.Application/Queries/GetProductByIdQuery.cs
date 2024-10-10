using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;
}
