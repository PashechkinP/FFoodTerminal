using FFoodTerminal.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.ViewModels.Product;
using System.Runtime.ConstrainedExecution;

namespace FFoodTerminal.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<IEnumerable<ProductEntity>>> GetProductsService();

        Task<IBaseResponse<IEnumerable<ProductEntity>>> GetProductsService(string categoryName);

        Task<IBaseResponse<ProductViewModel>> GetProductService(long id);

        Task<BaseResponse<Dictionary<int, string>>> GetProductService(string term);

        Task<IBaseResponse<bool>> DeleteProductService(int id);

        Task<IBaseResponse<ProductEntity>> GetProductByNameService(string name);

        Task<IBaseResponse<ProductEntity>> EditProductService(int id, ProductViewModel model, byte[] imageData);

        Task<IBaseResponse<ProductEntity>> CreateProductService(ProductViewModel product, byte[] imageData);
    }
}
