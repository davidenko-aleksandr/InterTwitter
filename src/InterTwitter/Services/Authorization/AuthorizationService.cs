using System;
using System.Linq;
using System.Threading.Tasks;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.UserService;

namespace InterTwitter.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {       
        private readonly IUserService _userService;

        public AuthorizationService(IUserService userService)
        {
            _userService = userService;
        }

        #region -- IAuthorizationService Implementation --

        public async Task<AOResult<bool>> LogInAsync(string email, string password)
        {
            var result = new AOResult<bool>();

            try
            {
                var users = (await _userService.GetUsersAsync()).Result;
                var user = users.First(x => x.Email == email && x.Password == password);
                
                await Task.Delay(300);

                if (user != null)
                {
                    result.SetSuccess(true);
                }
                else
                {
                    result.SetFailure(false);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(LogInAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<bool>> SignUpAsync(string email, string name, string password)
        {
            var result = new AOResult<bool>();

            try
            {
                var users = (await _userService.GetUsersAsync()).Result;
                var user = users.First(x => x.Email.ToUpper() == email.ToUpper());
                
                await Task.Delay(300);

                if (user == null)
                {
                    await _userService.AddUserAsync(new User()
                    {
                        Email = email,
                        Name = name,
                        Password = password,
                    });
                    result.SetSuccess(true);
                }
                else
                {
                    result.SetFailure(false);
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(SignUpAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
