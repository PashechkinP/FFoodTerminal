using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Enum;
using FFoodTerminal.Domain.Response;
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
        public async Task<IBaseResponse<IEnumerable<ProductEntity>>> GetProducts()
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
                    DescriptionError = $"[GetCars] : {ex.Message}"
                };
            }
        }
    }
}
