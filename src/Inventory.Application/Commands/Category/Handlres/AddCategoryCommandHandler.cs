using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;


namespace Inventory.Application.Commands.Category.Handlres
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, ResponseResult>
    {
        private readonly ICategoryCommandRepository _categoryRepository;

        public AddCategoryCommandHandler(ICategoryCommandRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new CategoryModel
                {
                    Name = request.Product.Name
                };
                await _categoryRepository.AddAsync(category);

                return new ResponseResult() { Success = true, Message = "Registro insertado correctamente" };
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Success = false, Message = ex.Message };
            }
        }
    }
}
