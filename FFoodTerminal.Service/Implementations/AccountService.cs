using FFoodTerminal.DataAccessLayer.Interfaces;
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

namespace FFoodTerminal.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<UserEntity> _userRepository;
        private ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<UserEntity> userRepository,
            ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
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
