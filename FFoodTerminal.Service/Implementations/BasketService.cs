using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Enum;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.Order;
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
    public class BasketService : IBasketService
    {
        private readonly IBaseRepository<UserEntity> _userRepository;
        private readonly IBaseRepository<ProductEntity> _productRepository;

        public BasketService(IBaseRepository<UserEntity> userRepository, IBaseRepository<ProductEntity> productRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        DescriptionError = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var orders = user.Basket?.Orders;
                var response = from p in orders
                               join c in _productRepository.GetAll() on p.ProductEntityId equals c.Id
                               select new OrderViewModel()
                               {
                                   Id = p.Id,
                                   ProductName = c.Name,
                                   ProductDescription = c.Description,
                                   ProductCategory = c.Category,/*.GetDisplayName(),*/ // Для этого метода надо переделать категории в отдельный класс enum с аннотациями, по принципу ролей
                                   ProductPrice = c.Price,
                                   Image = c.Avatar
                               };

                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    DescriptionError = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<OrderViewModel>> GetItem(string userName, long id)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        DescriptionError = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var orders = user.Basket?.Orders.Where(x => x.Id == id).ToList();
                if (orders == null || orders.Count == 0)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        DescriptionError = "Заказов нет",
                        StatusCode = StatusCode.OrderNotFound
                    };
                }

                var response = (from p in orders
                                join c in _productRepository.GetAll() on p.ProductEntityId equals c.Id
                                select new OrderViewModel()
                                {
                                    Id = p.Id,
                                    ProductName = c.Name,
                                    ProductDescription = c.Description,
                                    ProductCategory = c.Category,/*.GetDisplayName(),*/ // Для этого метода надо переделать категории в отдельный класс enum с аннотациями, по принципу ролей
                                    ProductPrice = c.Price,
                                    Address = p.Address,
                                    FirstName = p.FirstName,
                                    LastName = p.LastName,
                                    DateCreate = p.DateCreated.ToLongDateString(),
                                    Image = c.Avatar
                                }).FirstOrDefault();

                return new BaseResponse<OrderViewModel>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderViewModel>()
                {
                    DescriptionError = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
