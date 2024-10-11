using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class InventoryQueryRepository : IInventoryQueryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InventoryQueryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<InventoryModel> GetByIdAsync(int id)
        {
            return await _dbContext.Inventory.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<InventoryModel>> GetAllAsync()
        {
            return await _dbContext.Inventory.ToListAsync();
        }
    }
}
