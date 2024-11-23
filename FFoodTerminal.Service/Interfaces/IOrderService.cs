using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> Create(CreateOrderViewModel model);


        Task<IBaseResponse<bool>> Delete(long id);
    }
}
