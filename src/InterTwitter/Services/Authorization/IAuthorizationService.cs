using System.Threading.Tasks;
using InterTwitter.Helpers;
using InterTwitter.Models;

namespace InterTwitter.Services.Authorization
{
    public interface IAuthorizationService
    {
<<<<<<< HEAD
        bool IsAuthorized { get; }
        int AuthorizedUserId { get; }
=======
>>>>>>> 4f369e775b96684cbf34e26f8782cdc53cdbe0c1
        Task<AOResult<bool>> LogInAsync(string email, string password);
        Task<AOResult<bool>> SignUpAsync(string email, string name, string password);
        Task<AOResult<bool>> LogOutAsync();
        Task<AOResult<UserModel>> GetAuthorizedUserAsync();
        Task<AOResult<bool>> CheckUserEmail(string email);
    }
}
