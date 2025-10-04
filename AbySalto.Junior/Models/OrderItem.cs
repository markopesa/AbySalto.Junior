using AbySalto.Junior.Models.Base;

namespace AbySalto.Junior.Models
{
    public class OrderItem : BaseEntity<int>
    {
        public int OrderId { get; set; }
        public string ItemName { get; set; } 
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Total => Quantity * Price;

        public Order? Order { get; set; } 
    }
}
