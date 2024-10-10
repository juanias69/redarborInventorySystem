using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands
{
    public record UpdateProductCommand(ProductDto Product) : IRequest<ResponseResult>;
}
