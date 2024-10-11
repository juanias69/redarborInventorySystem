
namespace Inventory.Domain.Entities
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
