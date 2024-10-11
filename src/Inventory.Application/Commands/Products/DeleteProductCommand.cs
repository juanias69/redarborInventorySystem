using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands.Products
{
    public record DeleteProductCommand(int Id) : IRequest<ResponseResult>;
}
