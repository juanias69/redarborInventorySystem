using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands
{
    public record AddProductCommand(ProductDto Product) : IRequest<ResponseResult>;

}
    