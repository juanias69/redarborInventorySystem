using Inventory.Application.DTOs;
using MediatR;

namespace Inventory.Application.Queries.Inventories
{
    public record GetInventarytByProductIdQuery(int Id) : IRequest<InventoryDto>;
}
