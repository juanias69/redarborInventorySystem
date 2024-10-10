using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands
{
    public class DeleteProductCommand : IRequest<ResponseResult>
    {
        public int Id { get; set; }

    }
}
