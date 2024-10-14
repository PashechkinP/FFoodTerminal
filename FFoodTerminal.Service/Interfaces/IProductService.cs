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

        Task<IBaseResponse<ProductViewModel>> GetProductService(int id);

        Task<IBaseResponse<bool>> DeleteProductService(int id);

        Task<IBaseResponse<ProductEntity>> GetProductByNameService(string name);

        Task<IBaseResponse<ProductEntity>> EditProductService(int id, ProductViewModel model);

        Task<IBaseResponse<ProductEntity>> CreateProductService(ProductViewModel car, byte[] imageData);
    }
}
