using AbySalto.Junior.Repositories;
using AbySalto.Junior.ViewModels;
using AbySalto.Junior.ViewModels.Shared;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public RestaurantController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = new IndexOrderViewModel
                {
                    SearchFilter = new OrderSearchFilter()
                };

                await viewModel.PrepareData(_orderRepository);
                return View(viewModel);
            }
            catch (Exception e)
            {

                var errorModel = new ErrorViewModel
                {
                    Message = "Greška prilikom dohvaćanja narudžbi",
                    Url = Request.GetDisplayUrl(),
                };

                return View("Error", errorModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> OrderDetailsModal(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdWithDetailsAsync(id);
                if (order == null)
                    return BadRequest("Narudžba nije pronađena");

                var viewModel = new OrderDetailsViewModel
                {
                    Order = order
                };

                return PartialView("_OrderDetailsModal", viewModel);
            }
            catch (Exception e)
            {
                return BadRequest("Greška prilikom dohvaćanja detalja narudžbe");
            }
        }
    }
}
