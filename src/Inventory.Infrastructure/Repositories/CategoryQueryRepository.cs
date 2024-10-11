using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryQueryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }
}
