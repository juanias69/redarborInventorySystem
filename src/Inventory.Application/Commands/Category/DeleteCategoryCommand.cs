using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Commands.Category
{
    public record DeleteCategoryCommand(int Id) : IRequest<ResponseResult>;
}
