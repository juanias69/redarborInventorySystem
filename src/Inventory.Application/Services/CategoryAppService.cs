using Inventory.Application.Commands.Category;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Inventory.Application.Queries.Categories;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IMediator _mediator;

        public CategoryAppService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ResponseResult> AddCategoryAsync(CategoryDto categoryDto)
        {
            var command = new AddCategoryCommand(categoryDto);
            return await _mediator.Send(command);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var query = new GetAllCategoriesQuery();
            return await _mediator.Send(query);
        }

        public async Task<ResponseResult> DeleteCategoryAsync(int id)
        {
            var query = new DeleteCategoryCommand(id);
            return await _mediator.Send(query);
        }
    }
}
