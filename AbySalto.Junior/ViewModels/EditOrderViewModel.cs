using AbySalto.Junior.Models;
using AbySalto.Junior.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AbySalto.Junior.ViewModels
{
    public class EditOrderViewModel
    {
        public int Id { get; set; }

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

        public List<SelectListItem> OrderStatuses { get; set; } = new();
        public List<SelectListItem> PaymentMethods { get; set; } = new();

        public List<EditOrderItemViewModel> Items { get; set; } = new();

        public async Task PrepareSelectLists(IOrderRepository orderRepository)
        {
            var orderStatuses = await orderRepository.GetAllOrderStatusesAsync();
            OrderStatuses = orderStatuses.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == OrderStatusId
            }).ToList();

            var paymentMethods = await orderRepository.GetAllPaymentMethodsAsync();
            PaymentMethods = paymentMethods.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == PaymentMethodId
            }).ToList();
        }

        public async Task UpdateOrder(IOrderRepository orderRepository)
        {
            var order = await orderRepository.GetByIdWithDetailsAsync(Id);
            if (order == null)
                throw new ArgumentException("Narudžba nije pronađena");

            order.CustomerName = CustomerName;
            order.DeliveryAddress = DeliveryAddress;
            order.ContactNumber = ContactNumber;
            order.Note = Note;
            order.OrderStatusId = OrderStatusId;
            order.PaymentMethodId = PaymentMethodId;
            order.Currency = Currency;

            order.Items.Clear();
            order.Items = Items.Where(x => !string.IsNullOrEmpty(x.ItemName) && x.Quantity > 0)
                               .Select(x => new OrderItem
                               {
                                   OrderId = Id,
                                   ItemName = x.ItemName,
                                   Quantity = x.Quantity,
                                   Price = x.Price,
                                   DateCreated = DateTime.UtcNow
                               }).ToList();

            order.TotalAmount = order.Items.Sum(x => x.Quantity * x.Price);

            await orderRepository.SaveChangesAsync();
        }
    }

    public class EditOrderItemViewModel
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
