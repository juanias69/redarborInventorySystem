
using Inventory.Application.Commands.Products;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Commands.Category.Handlres
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResponseResult>
    {
        private readonly ICategoryCommandRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryCommandRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _categoryRepository.DeleteAsync(request.Id);

                return new ResponseResult() { Success = true, Message = "Registro eliminado correctamente" };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Message = ex.Message };
            }
        }
    }
}
