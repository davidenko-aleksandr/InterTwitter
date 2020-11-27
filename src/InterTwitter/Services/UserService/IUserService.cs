using InterTwitter.Helpers;
using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public interface IUserService
    {
        Task<AOResult<List<UserModel>>> GetUsersAsync();

        Task<AOResult<UserModel>> GetUserAsync(int id);

        Task<AOResult<bool>> AddUserAsync(UserModel user);

        Task<AOResult<bool>> UpdateUserAsync(UserModel user);     
    }
}
