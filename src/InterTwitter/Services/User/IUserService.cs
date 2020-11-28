using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public interface IUserService
    {
        Task<AOResult<List<UserViewModel>>> GetUsersAsync();

        Task<AOResult<bool>> AddUserAsync(UserModel user);

        Task<AOResult<bool>> UpdateUserAsync(UserViewModel user);     
    }
}
