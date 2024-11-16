using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.DataAccessLayer.Repositories;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Enum;
using FFoodTerminal.Domain.Helpers;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.Account;
using FFoodTerminal.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FFoodTerminal.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<UserEntity> _userRepository;
        private readonly IBaseRepository<ProfileEntity> _profileRepository;
        private ILogger<AccountService> _logger;
        private readonly IBaseRepository<Basket> _basketRepository;

        public AccountService(IBaseRepository<UserEntity> userRepository,
            ILogger<AccountService> logger, IBaseRepository<ProfileEntity> profileRepository, IBaseRepository<Basket> basketRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _logger = logger;
            _basketRepository = basketRepository;
        }
        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        DescriptionError = "Такой пользователь уже есть",
                    };
                }
                user = new UserEntity()
                {
                    Name = model.Name,
                    Role = Role.User,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };
                await _userRepository.Create(user);

                var profile = new ProfileEntity()
                {
                    UserEntityId = user.Id,
                };

                await _profileRepository.Create(profile);

                var basket = new Basket()
                {
                    UserEntityId= user.Id,
                };

                await _basketRepository.Create(basket);


                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    DescriptionError = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    DescriptionError = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        DescriptionError = "Пользователь не найден"
                    };
                }
                if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        DescriptionError = "Неверный пароль"
                    };
                }
                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    DescriptionError = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.UserName);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        DescriptionError = "Пользователь не найден"
                    };
                }

                user.Password = HashPasswordHelper.HashPassowrd(model.NewPassword);
                await _userRepository.Update(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    DescriptionError = "Пароль обновлен"
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ChangePassword]: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    DescriptionError = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        private ClaimsIdentity Authenticate(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
