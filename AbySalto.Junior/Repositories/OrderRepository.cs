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

        public async Task<List<OrderStatus>> GetAllOrderStatusesAsync()
        {
            return await _context.OrderStatuses
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _context.PaymentMethods
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
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
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
