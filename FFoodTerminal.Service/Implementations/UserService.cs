﻿using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Enum;
using FFoodTerminal.Domain.Helpers;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.User;
using FFoodTerminal.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FFoodTerminal.Service.Implementations
{
    public class UserService : IUserService
    {
        //private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<ProfileEntity> _proFileRepository;
        private readonly IBaseRepository<UserEntity> _userRepository;

        public UserService(IBaseRepository<UserEntity> userRepository, IBaseRepository<ProfileEntity> proFileRepository)
        {
            //_logger = logger;
            _userRepository = userRepository;
            _proFileRepository = proFileRepository;
        }

        public async Task<IBaseResponse<UserEntity>> Create(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<UserEntity>()
                    {
                        DescriptionError = "Пользователь с таким логином уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }
                user = new UserEntity()
                {
                    Name = model.Name,
                    Role = Enum.Parse<Role>(model.Role),
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };

                await _userRepository.Create(user);

                var profile = new ProfileEntity()
                {
                    Address = string.Empty,
                    Age = 0,
                    UserEntityId = user.Id,
                };

                await _proFileRepository.Create(profile);

                return new BaseResponse<UserEntity>()
                {
                    Data = user,
                    DescriptionError = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");

                return new BaseResponse<UserEntity>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    DescriptionError = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    DescriptionError = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

                //_logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");

                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");

                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    DescriptionError = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUser(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userRepository.Delete(user);

                //_logger.LogInformation($"[UserService.DeleteUser] пользователь удален");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");

                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    DescriptionError = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
