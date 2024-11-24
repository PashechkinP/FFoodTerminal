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
        public async Task<IActionResult> GetProductsSport(string categoryName)
        {
            categoryName = "Спорт";
            var response = await _productService.GetProductsService(categoryName);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("GetProducts", response.Data.ToList());
            }
            return View("Error", $"{response.DescriptionError}");
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsTourist(string categoryName)
        {
            categoryName = "Турист";
            var response = await _productService.GetProductsService(categoryName);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("GetProducts", response.Data.ToList());
            }
            return View("Error", $"{response.DescriptionError}");
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsCruiser(string categoryName)
        {
            categoryName = "Круизер";
            var response = await _productService.GetProductsService(categoryName);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View("GetProducts", response.Data.ToList());
            }
            return View("Error", $"{response.DescriptionError}");
        }

        public IActionResult Compare() => PartialView();

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

        [HttpPost]
        public async Task<IActionResult> GetProduct(string term)
        {
            var response = await _productService.GetProductService(term);
            return Json(response.Data);
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
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    await _productService.EditProductService(model.Id, model, imageData);
                }
                return RedirectToAction("GetProducts");
            }
            return View();
        }

    }
}
