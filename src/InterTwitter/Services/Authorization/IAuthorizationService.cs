using System;
using System.Threading.Tasks;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.ViewModels;

namespace InterTwitter.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }
        int AuthorizedUserId { get; }
        Task<AOResult<bool>> LogInAsync(string email, string password);
        Task<AOResult<bool>> SignUpAsync(string email, string name, string password);
        Task<AOResult<bool>> LogOutAsync();
        Task<AOResult<UserViewModel>> GetAuthorizedUserAsync();
        Task<AOResult<bool>> CheckUserEmail(string email);
    }
}
