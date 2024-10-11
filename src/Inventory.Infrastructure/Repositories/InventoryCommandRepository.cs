using Dapper;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using System.Data;


namespace Inventory.Infrastructure.Repositories
{
    public class InventoryCommandRepository : IInventoryCommandRepository
    {
        private readonly IDbConnection _dbConnection;

        public InventoryCommandRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(InventoryModel inventory)
        {
            var query = "INSERT INTO Inventory (ProductId, Quantity, EntryDate) VALUES (@ProductId, @Quantity, @EntryDate)";
            await _dbConnection.ExecuteAsync(query, inventory);
        }

        public async Task UpdateAsync(InventoryModel inventory)
        {
            var query = "UPDATE Inventory SET ProductId = @ProductId, Quantity = @Quantity, EntryDate = @EntryDate WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, inventory);
        }
    }
}
