using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.UserService
{
    public interface IUserService
    {
        Task<AOResult<IEnumerable<UserModel>>> GetUsersAsync();

        Task<AOResult<UserModel>> GetUserAsync(int id);

        Task<AOResult<bool>> AddUserAsync(UserModel user);

        Task<AOResult<bool>> UpdateUserAsync(UserModel user);
    }
}
