using Inventory.Application.DTOs;
using MediatR;

namespace Inventory.Application.Queries.Categories
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;
}
