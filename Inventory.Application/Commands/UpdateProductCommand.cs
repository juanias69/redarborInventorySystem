using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands
{
    public class UpdateProductCommand : IRequest<ResponseResult>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
    }
}
