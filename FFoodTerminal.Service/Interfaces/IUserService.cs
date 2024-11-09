using FFoodTerminal.Domain.Entities;
using FFoodTerminal.Domain.Response;
using FFoodTerminal.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFoodTerminal.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<UserEntity>> Create(UserViewModel model);

        BaseResponse<Dictionary<int, string>> GetRoles();

        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();

        Task<IBaseResponse<bool>> DeleteUser(long id);
    }
}
