using AbySalto.Junior.Models.Base;

namespace AbySalto.Junior.Models
{
    public class Order : BaseEntity<int>
    {
        public string CustomerName { get; set; } 
        public DateTime OrderTime { get; set; }
        public string DeliveryAddress { get; set; } 
        public string ContactNumber { get; set; } 
        public string? Note { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmount { get; set; }

        public int OrderStatusId { get; set; }
        public int PaymentMethodId { get; set; }

        public OrderStatus OrderStatus { get; set; } = null!;
        public PaymentMethod PaymentMethod { get; set; } = null!;
        public List<OrderItem> Items { get; set; } = new();
    }
}
