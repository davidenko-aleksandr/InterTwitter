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
        Task<AOResult<List<User>>> GetUsersAsync();

        Task<AOResult<bool>> AddUserAsync(User user);

        Task<AOResult<bool>> UpdateUserAsync(User user);     
    }
}
