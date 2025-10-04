using AbySalto.Junior.Models.Base;

namespace AbySalto.Junior.Models
{
    public class OrderStatus : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}
