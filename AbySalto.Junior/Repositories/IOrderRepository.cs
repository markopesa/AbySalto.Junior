using AbySalto.Junior.Models;

namespace AbySalto.Junior.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdWithDetailsAsync(int id);
        Task<List<OrderStatus>> GetAllOrderStatusesAsync();
        Task<List<PaymentMethod>> GetAllPaymentMethodsAsync();
        Task<Order> AddAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}
