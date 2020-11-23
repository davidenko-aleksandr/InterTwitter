using InterTwitter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<int> AddOrUpdateAsync(User user);

        Task<int> DeleteUserAsync(User user);     
    }
}
