using System;
using System.Threading.Tasks;
using InterTwitter.Helpers;

namespace InterTwitter.Services.Authorization
{
    public interface IAuthorizationService
    {
        Task<AOResult<bool>> LogInAsync(string email, string password);
        Task<AOResult<bool>> SignUpAsync(string email, string name, string password);
    }
}
