using AbySalto.Junior.Models;
using AbySalto.Junior.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AbySalto.Junior.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required(ErrorMessage = "Ime kupca je obavezno")]
        [MaxLength(100, ErrorMessage = "Ime kupca može imati maksimalno 100 znakova")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adresa dostave je obavezna")]
        [MaxLength(200, ErrorMessage = "Adresa može imati maksimalno 200 znakova")]
        public string DeliveryAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kontakt broj je obavezan")]
        [MaxLength(20, ErrorMessage = "Kontakt broj može imati maksimalno 20 znakova")]
        public string ContactNumber { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Napomena može imati maksimalno 500 znakova")]
        public string? Note { get; set; }

        [Required(ErrorMessage = "Status je obavezan")]
        public int OrderStatusId { get; set; }

        [Required(ErrorMessage = "Način plaćanja je obavezan")]
        public int PaymentMethodId { get; set; }

        public string Currency { get; set; } = "EUR";

        public SelectList OrderStatuses { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());
        public SelectList PaymentMethods { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

        public List<CreateOrderItemViewModel> Items { get; set; } = new()
        {
            new CreateOrderItemViewModel() 
        };

        public async Task PrepareSelectLists(IOrderRepository orderRepository)
        {
            var orderStatuses = await orderRepository.GetAllOrderStatusesAsync();
            var orderStatusItems = orderStatuses.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Code == "PENDING"
            }).ToList();

            var paymentMethods = await orderRepository.GetAllPaymentMethodsAsync();
            var paymentMethodItems = paymentMethods.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            OrderStatuses = new SelectList(orderStatusItems, "Value", "Text");
            PaymentMethods = new SelectList(paymentMethodItems, "Value", "Text");

            if (OrderStatusId == 0)
            {
                var defaultStatus = orderStatuses.FirstOrDefault(x => x.Code == "PENDING");
                if (defaultStatus != null)
                    OrderStatusId = defaultStatus.Id;
            }
        }

        public async Task SaveOrder(IOrderRepository orderRepository)
        {
            var order = new Order
            {
                CustomerName = CustomerName,
                DeliveryAddress = DeliveryAddress,
                ContactNumber = ContactNumber,
                Note = Note,
                OrderStatusId = OrderStatusId,
                PaymentMethodId = PaymentMethodId,
                Currency = Currency,
                OrderTime = DateTime.Now,
                DateCreated = DateTime.UtcNow,
                Items = Items.Where(x => !string.IsNullOrEmpty(x.ItemName) && x.Quantity > 0)
                           .Select(x => new OrderItem
                           {
                               ItemName = x.ItemName,
                               Quantity = x.Quantity,
                               Price = x.Price,
                               DateCreated = DateTime.UtcNow
                           }).ToList()
            };

            order.TotalAmount = order.Items.Sum(x => x.Quantity * x.Price);

            await orderRepository.AddAsync(order);
        }
    }

    public class CreateOrderItemViewModel
    {
        [Required(ErrorMessage = "Naziv stavke je obavezan")]
        [MaxLength(100, ErrorMessage = "Naziv stavke može imati maksimalno 100 znakova")]
        public string ItemName { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Količina mora biti između 1 i 100")]
        public int Quantity { get; set; } = 1;

        [Range(0.01, 10000, ErrorMessage = "Cijena mora biti između 0.01 i 10000")]
        public decimal Price { get; set; }

        public decimal Total => Quantity * Price;
    }
}