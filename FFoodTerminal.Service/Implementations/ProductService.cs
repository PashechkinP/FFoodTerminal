﻿using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Enum;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.Product;
using FFoodTerminal.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<ProductEntity> _productRepository;

        public ProductService(IBaseRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IBaseResponse<ProductViewModel>> GetProductService(int id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<ProductViewModel>()
                    {
                        DescriptionError = "Пользователь не найден",
                        StatusCode = StatusCode.ProductNotFound,
                    };
                }

                var data = new ProductViewModel()
                {
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Price = product.Price,
                    Image = product.Avatar,
                };

                return new BaseResponse<ProductViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    DescriptionError = $"[GetProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProductEntity>> CreateProductService(ProductViewModel model, byte[] imageData)
        {
            try
            {
                var product = new ProductEntity()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Category = model.Category,
                    Price = model.Price,
                    Avatar = imageData
                };
                await _productRepository.Create(product);
                return new BaseResponse<ProductEntity>()
                {
                    StatusCode = StatusCode.OK,
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductEntity>()
                {
                    DescriptionError = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteProductService(int id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<bool>()
                    {
                        DescriptionError = "User not found",
                        StatusCode = StatusCode.ProductNotFound,
                        Data = false
                    };
                }

                await _productRepository.Delete(product);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    DescriptionError = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProductEntity>> GetProductByNameService(string name)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
                if (product == null)
                {
                    return new BaseResponse<ProductEntity>()
                    {
                        DescriptionError = "User not found",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }

                return new BaseResponse<ProductEntity>()
                {
                    Data = product,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductEntity>()
                {
                    DescriptionError = $"[GetProductByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProductEntity>> EditProductService(int id, ProductViewModel model, byte[] imageData)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<ProductEntity>()
                    {
                        DescriptionError = "Product not found",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.Category = model.Category;
                product.Price = model.Price;
                product.Avatar = imageData;
                await _productRepository.Update(product);


                return new BaseResponse<ProductEntity>()
                {
                    Data = product,
                    StatusCode = StatusCode.OK,
                };
                
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductEntity>()
                {
                    DescriptionError = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<ProductEntity>>> GetProductsService()
        {
            try
            {
                var products = _productRepository.GetAll();
                if (!products.Any())
                {
                    return new BaseResponse<IEnumerable<ProductEntity>>()
                    {
                        DescriptionError = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<ProductEntity>>()
                {
                    Data = products,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ProductEntity>>()
                {
                    DescriptionError = $"[GetProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
