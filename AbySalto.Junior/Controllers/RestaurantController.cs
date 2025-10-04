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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var viewModel = new CreateOrderViewModel();
                await viewModel.PrepareSelectLists(_orderRepository); 

                return View(viewModel);
            }
            catch (Exception e)
            {
                var errorModel = new ErrorViewModel
                {
                    Message = "Greška prilikom učitavanja forme za novu narudžbu",
                    Url = Request.GetDisplayUrl()
                };
                return View("Error", errorModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await model.PrepareSelectLists(_orderRepository);
                    return View(model);
                }

                await model.SaveOrder(_orderRepository);

                TempData["SuccessMessage"] = "Narudžba je uspješno kreirana!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                await model.PrepareSelectLists(_orderRepository);
                ModelState.AddModelError("", "Greška prilikom snimanja narudžbe");
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult GetOrderItemPartial(int index)
        {
            try
            {
                var viewModel = new CreateOrderItemViewModel();
                ViewBag.Index = index;

                return PartialView("_OrderItemPartial", viewModel);
            }
            catch (Exception e)
            {
                return BadRequest("Greška prilikom dohvaćanja stavke");
            }
        }
    }
}
