using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
