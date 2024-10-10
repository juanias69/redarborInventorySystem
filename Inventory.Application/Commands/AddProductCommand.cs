using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands
{
    public class AddProductCommand : IRequest<ResponseResult>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
    }
}
