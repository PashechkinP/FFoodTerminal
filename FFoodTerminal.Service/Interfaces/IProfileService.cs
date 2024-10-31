using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FFoodTerminal.Service.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string userName);

        Task<BaseResponse<ProfileEntity>> Save(ProfileViewModel model);
    }
}
