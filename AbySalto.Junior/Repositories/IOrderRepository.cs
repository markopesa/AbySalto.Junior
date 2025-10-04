using AbySalto.Junior.Models;

namespace AbySalto.Junior.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdWithDetailsAsync(int id);
        Task<Order?> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}
