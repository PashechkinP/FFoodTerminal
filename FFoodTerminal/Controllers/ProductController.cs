using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.ViewModels.Product;
using FFoodTerminal.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
            var response = await _productService.GetProductsService();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return View("Error", $"{response.DescriptionError}");
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await _productService.GetProductService(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.DescriptionError}");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProductService(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetProducts");
            }
            return View("Error", $"{response.DescriptionError}");
        }

        [HttpGet]
        public async Task<IActionResult> SaveProduct(int id)
        {
            if (id == 0)

                return View();

            var response = await _productService.GetProductService(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.DescriptionError}");
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    await _productService.CreateProductService(model, imageData);
                }
                else
                {
                    await _productService.EditProductService(model.Id, model);
                }
                return RedirectToAction("GetProducts");
            }
            return View();
        }

    }
}
