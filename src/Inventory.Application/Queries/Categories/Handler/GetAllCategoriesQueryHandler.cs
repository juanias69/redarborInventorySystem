using Inventory.Application.DTOs;
using Inventory.Domain.Interfaces;
using MediatR;

namespace Inventory.Application.Queries.Categories.Handler
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {

        private readonly ICategoryQueryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryQueryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<CategoryDto> ltCategories = new();
                var productos = await _categoryRepository.GetAllAsync();
                if (productos != null)
                {
                    ltCategories = productos.Select(p => new CategoryDto
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToList();
                }

                return ltCategories;
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("Error al obtener la informacion", ex.InnerException);
            }
        }
    }
}
