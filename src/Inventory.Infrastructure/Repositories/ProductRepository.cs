using Dapper;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using System.Data;


namespace Inventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(Product product)
        {
            var query = "INSERT INTO Products (Name, Description, CategoryId, Price) VALUES (@Name, @Description, @CategoryId, @Price)";
            await _dbConnection.ExecuteAsync(query, product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var query = "SELECT * FROM Products";
            return await _dbConnection.QueryAsync<Product>(query);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Products WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task UpdateAsync(Product product)
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
