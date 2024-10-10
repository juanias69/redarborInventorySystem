
namespace Inventory.Domain.Entities
{
    public class Inventorys
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
