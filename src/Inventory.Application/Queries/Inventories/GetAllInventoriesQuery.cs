
using Inventory.Application.DTOs;
using MediatR;

namespace Inventory.Application.Queries.Inventories
{
    public record GetAllInventoriesQuery : IRequest<IEnumerable<InventoryDto>>;
}
