using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands.Products
{
    public record UpdateProductCommand(ProductDto Product) : IRequest<ResponseResult>;
}
