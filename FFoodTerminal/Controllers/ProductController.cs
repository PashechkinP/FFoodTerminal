using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FFoodTerminal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService=productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var response = await _productService.GetProducts();
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }
    }
}
