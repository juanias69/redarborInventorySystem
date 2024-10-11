using Dapper;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using System.Data;

namespace Inventory.Infrastructure.Repositories
{
    public class ProductCommandRepository : IProductCommandRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductCommandRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(ProductModel product)
        {
            var query = "INSERT INTO Products (Name, Description, CategoryId, Price) VALUES (@Name, @Description, @CategoryId, @Price)";
            await _dbConnection.ExecuteAsync(query, product);
        }

        public async Task UpdateAsync(ProductModel product)
        {
            var query = "UPDATE Products SET Name = @Name, Description = @Description, CategoryId = @CategoryId, Price = @Price WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, product);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM Products WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
