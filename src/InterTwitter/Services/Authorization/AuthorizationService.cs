using System;
using System.Linq;
using System.Threading.Tasks;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;

namespace InterTwitter.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly ISettingsService _settingsService;

        public AuthorizationService(IUserService userService,
                                    ISettingsService settingsService)
        {
            _userService = userService;
            _settingsService = settingsService;
        }

        #region -- IAuthorizationService implementation --

        public bool IsAuthorized
        {
            get => _settingsService.UserEmail != string.Empty;
        }

        public async Task<AOResult<bool>> LogInAsync(string email, string password)
        {
            var result = new AOResult<bool>();

            try
            {
                var getUsersResult = await _userService.GetUsersAsync();

                if (getUsersResult.IsSuccess)
                {
                    var users = getUsersResult.Result;

                    var user = users.First(x => x.Email == email && x.Password == password);                

                    await Task.Delay(300);

                    if (user != null)
                    {
                        _settingsService.UserEmail = user.Email;
                   
                        result.SetSuccess(true);
                    }
                    else
                    {
                        result.SetFailure(false);
                    }                 
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
                var getUsersResult = await _userService.GetUsersAsync();
               
                if (getUsersResult.IsSuccess)
                {
                    var users = getUsersResult.Result;

                    var user = users.First(x => x.Email.ToUpper() == email.ToUpper());

                    await Task.Delay(300);

                    if (user == null)
                    {
                        user = new User()
                                   {
                                       Email = email,
                                       Name = name,
                                       Password = password,
                                   };

                        await _userService.AddUserAsync(user);

                        _settingsService.UserEmail = email;

                        result.SetSuccess(true);
                    }
                    else
                    {
                        result.SetFailure(false);
                    }
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

        public void LogOut()
        {
            _settingsService.ClearData();
        }

        #endregion
    }
}
