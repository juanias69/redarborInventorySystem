using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands.Products
{
    public record AddProductCommand(ProductDto Product) : IRequest<ResponseResult>;

}
