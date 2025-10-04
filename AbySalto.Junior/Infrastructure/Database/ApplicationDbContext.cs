using AbySalto.Junior.Models;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
       
         public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            SeedData(modelBuilder);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            // OrderStatus seed
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Name = "Na čekanju", Code = "PENDING", Description = "Narudžba je zaprimljena", DateCreated = new DateTime(2024, 10, 1, 10, 0, 0) },
                new OrderStatus { Id = 2, Name = "U pripremi", Code = "IN_PROGRESS", Description = "Narudžba se priprema", DateCreated = new DateTime(2024, 10, 1, 10, 0, 0) },
                new OrderStatus { Id = 3, Name = "Završena", Code = "COMPLETED", Description = "Narudžba je dovršena", DateCreated = new DateTime(2024, 10, 1, 10, 0, 0) }
            );

            // PaymentMethod seed
            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, Name = "Gotovina", Code = "CASH", Description = "Plaćanje gotovinom", DateCreated = new DateTime(2024, 10, 1, 10, 0, 0) },
                new PaymentMethod { Id = 2, Name = "Kartica", Code = "CARD", Description = "Plaćanje karticom", DateCreated = new DateTime(2024, 10, 1, 10, 0, 0) },
                new PaymentMethod { Id = 3, Name = "Online", Code = "ONLINE", Description = "Online plaćanje", DateCreated = new DateTime(2024, 10, 1, 10, 0, 0) }
            );

            // Orders seed
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerName = "Marko Marković",
                    OrderTime = new DateTime(2024, 10, 4, 17, 0, 0),
                    DeliveryAddress = "Ilica 10, Zagreb",
                    ContactNumber = "+385 91 123 4567",
                    Note = "Molim bez luka",
                    Currency = "EUR",
                    TotalAmount = 25.50m,
                    OrderStatusId = 2,
                    PaymentMethodId = 1,
                    DateCreated = new DateTime(2024, 10, 4, 15, 0, 0)
                },
                new Order
                {
                    Id = 2,
                    CustomerName = "Ana Anić",
                    OrderTime = new DateTime(2024, 10, 4, 18, 0, 0),
                    DeliveryAddress = "Maksimirska 15, Zagreb",
                    ContactNumber = "+385 92 234 5678",
                    Currency = "EUR",
                    TotalAmount = 42.75m,
                    OrderStatusId = 1,
                    PaymentMethodId = 2,
                    DateCreated = new DateTime(2024, 10, 4, 16, 0, 0)
                }
            );

            // OrderItems seed  
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, ItemName = "Pizza Margherita", Quantity = 1, Price = 12.50m, DateCreated = new DateTime(2024, 10, 4, 15, 0, 0) },
                new OrderItem { Id = 2, OrderId = 1, ItemName = "Coca Cola", Quantity = 2, Price = 6.50m, DateCreated = new DateTime(2024, 10, 4, 15, 0, 0) },
                new OrderItem { Id = 3, OrderId = 2, ItemName = "Burger Deluxe", Quantity = 1, Price = 18.00m, DateCreated = new DateTime(2024, 10, 4, 16, 0, 0) },
                new OrderItem { Id = 4, OrderId = 2, ItemName = "Pomfrit", Quantity = 2, Price = 8.00m, DateCreated = new DateTime(2024, 10, 4, 16, 0, 0) },
                new OrderItem { Id = 5, OrderId = 2, ItemName = "Sprite", Quantity = 1, Price = 3.75m, DateCreated = new DateTime(2024, 10, 4, 16, 0, 0) }
            );
        }
    }
}
