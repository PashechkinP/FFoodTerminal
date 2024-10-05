using FFoodTerminal.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.ViewModels.Product;

namespace FFoodTerminal.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<IEnumerable<ProductEntity>>> GetProductsService();
        Task<IBaseResponse<ProductEntity>> GetProductService(int id);
        Task<IBaseResponse<ProductEntity>> GetProductByNameService(string name);
        Task<IBaseResponse<ProductViewModel>> CreateProductService(ProductViewModel productViewModel);
        Task<IBaseResponse<bool>> DeleteProductService(int id);

        Task<IBaseResponse<ProductEntity>> EditProductService(int id, ProductViewModel productViewModel);
    }
}
