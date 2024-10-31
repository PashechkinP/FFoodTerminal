using FFoodTerminal.DataAccessLayer.Interfaces;
using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Enum;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.Profile;
using FFoodTerminal.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FFoodTerminal.Service.Implementations
{
    public class ProfileService: IProfileService
    {
        //private readonly ILogger<ProfileService> _logger;
        private readonly IBaseRepository<ProfileEntity> _profileRepository;

        public ProfileService(IBaseRepository<ProfileEntity> profileRepository) //(ILogger<ProfileService> logger)

        {
            _profileRepository = profileRepository;
            //_logger = logger;
        }

        public async Task<BaseResponse<ProfileViewModel>> GetProfile(string userName)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .Select(x => new ProfileViewModel()
                    {
                        Id = x.Id,
                        Address = x.Address,
                        Age = x.Age,
                        UserName = x.UserEntity.Name
                    })
                    .FirstOrDefaultAsync(x => x.UserName == userName);

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"[ProfileService.GetProfile] error: {ex.Message}");
                return new BaseResponse<ProfileViewModel>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    DescriptionError = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<ProfileEntity>> Save(ProfileViewModel model)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                profile.Address = model.Address;
                profile.Age = model.Age;

                await _profileRepository.Update(profile);

                return new BaseResponse<ProfileEntity>()
                {
                    Data = profile,
                    DescriptionError = "Данные обновлены",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"[ProfileService.Save] error: {ex.Message}");
                return new BaseResponse<ProfileEntity>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    DescriptionError = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
