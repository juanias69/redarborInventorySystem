using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
