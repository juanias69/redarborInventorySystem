using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductQueryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
    }
}
