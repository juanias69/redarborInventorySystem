using Dapper;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class CategoryCommandRepository : ICategoryCommandRepository
    {
        private readonly IDbConnection _dbConnection;

        public CategoryCommandRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(CategoryModel category)
        {
            var query = "INSERT INTO Categories (Name) VALUES (@Name)";
            await _dbConnection.ExecuteAsync(query, category);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Categories WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
