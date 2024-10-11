using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands.Inventory
{
    public record AddInventoryCommand(InventoryDto inventory) : IRequest<ResponseResult>;

}
