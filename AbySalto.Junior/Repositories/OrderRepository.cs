using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IApplicationDbContext _context;

        public OrderRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Order> AddAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentMethod)
                .Include(o => o.Items)
                .Where(o => !o.IsDeleted)
                .ToListAsync();
        }
        public async Task<Order?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderStatus)
                .Include(o => o.PaymentMethod)
                .Include(o => o.Items)
                .Where(o => !o.IsDeleted && o.Id == id)
                .FirstOrDefaultAsync();
        }
        public Task<Order?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
