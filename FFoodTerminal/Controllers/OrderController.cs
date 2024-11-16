using FFoodTerminal.Domain.ViewModels.Order;
using FFoodTerminal.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FFoodTerminal.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult CreateOrder(long id)
        {
            var orderModel = new CreateOrderViewModel()
            {
                ProductEntityId = id,
                Login = User.Identity.Name,
                Quantity = 0
            };
            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _orderService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.DescriptionError });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _orderService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Error", $"{response.DescriptionError}");
        }
    }
}
