using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Enum;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.Product;
using FFoodTerminal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IBaseResponse<ProductEntity>> GetProductService(int id)
        {
            var baseResponse  = new BaseResponse<ProductEntity>();
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.DescriptionError = "Product not found";
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    return baseResponse;
                }

                baseResponse.Data = product;

                baseResponse.StatusCode = StatusCode.OK;
                
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductEntity>()
                {
                    DescriptionError = $"[GetCarService] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<ProductEntity>> GetProductByNameService(string name)
        {
            var baseResponse = new BaseResponse<ProductEntity>();
            try
            {
                var product = await _productRepository.GetByName(name);
                if (product == null)
                {
                    baseResponse.DescriptionError = "Product not found";
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    return baseResponse;
                }

                baseResponse.Data = product;

                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductEntity>()
                {
                    DescriptionError = $"[GetProductByNameService] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        // Возможно здесь надо передавать ProductViewModel
        public async Task<IBaseResponse<IEnumerable<ProductEntity>>> GetProductsService()
        {
            var baseResponse = new BaseResponse<IEnumerable<ProductEntity>>();
            try
            {
                var products = await _productRepository.Select();
                if (products.Count == 0)
                {
                    baseResponse.DescriptionError = "No elements found";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = products;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ProductEntity>>()
                {
                    DescriptionError = $"[GetCarsSevice] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,

                };
            }
        }

        public async Task<IBaseResponse<ProductViewModel>> CreateProductService(ProductViewModel productViewModel)
        {
            var baseResponse = new BaseResponse<ProductViewModel>();
            try
            {
                var product = new ProductEntity()
                {
                    Name = productViewModel.Name,
                    Description = productViewModel.Description,
                    Category = productViewModel.Category,
                    Price = productViewModel.Price,
                };

                await _productRepository.Create(product);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    DescriptionError = $"[CreateProductService] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,

                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteProductService(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.DescriptionError = "Product not found";
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    return baseResponse;
                }

                await _productRepository.Delete(product);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    DescriptionError = $"[DeleteProductSevice] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,

                };
            }
        }


        public async Task<IBaseResponse<ProductEntity>> EditProductService(int id, ProductViewModel productViewModel)
        {
            var baseResponse = new BaseResponse<ProductEntity>();
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    baseResponse.DescriptionError = "Product not found";
                    return baseResponse;
                }

                product.Name = productViewModel.Name;
                product.Description = productViewModel.Description;
                product.Category = productViewModel.Category;
                product.Price = productViewModel.Price;

                await _productRepository.Update(product);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductEntity>()
                {
                    DescriptionError = $"[EditCarsSevice] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError,

                };
            }
        }

    }
}
