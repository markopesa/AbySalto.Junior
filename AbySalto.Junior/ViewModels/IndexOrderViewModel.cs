using AbySalto.Junior.Models;
using AbySalto.Junior.Repositories;

namespace AbySalto.Junior.ViewModels
{
    public class IndexOrderViewModel
    {
        public List<Order> Orders { get; set; } = new();
        public OrderSearchFilter SearchFilter { get; set; } = new();

        public async Task PrepareData(IOrderRepository orderRepository)
        {
            try
            {
                Orders = await orderRepository.GetAllOrdersAsync();
            }
            catch (Exception ex)
            {
                //tu bi logao error inace
                Orders = new List<Order>();
            }
        }
    }
}
