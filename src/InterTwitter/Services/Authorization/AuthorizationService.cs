using System;
using System.Linq;
using System.Threading.Tasks;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Settings;
using InterTwitter.Extensions;
using InterTwitter.Services.UserService;
using InterTwitter.ViewModels;

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
            get => _settingsService.AuthorizedUserId != Constants.NoAuthorizedUser;
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
                        _settingsService.AuthorizedUserId = user.Id;

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

                    var user = users.First(x => x.Email.ToUpper() == email.ToUpper()).ToUserModel();

                    await Task.Delay(300);

                    if (user == null)
                    {
                        user = new UserModel()
                        {
                            Id = users.Count,
                            Email = email,
                            Name = name,
                            Password = password,
                            Picture = Constants.DefaultProfilePicture,
                    };

                        await _userService.AddUserAsync(user);

                        _settingsService.AuthorizedUserId = user.Id;

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

        public async Task<AOResult<bool>> LogOutAsync()
        {
            var result = new AOResult<bool>();

            try
            {
                _settingsService.ResetSettings();

                await Task.Delay(300);

                result.SetSuccess(true);
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(LogOutAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<UserViewModel>> GetAuthorizedUserAsync()
        {
            var result = new AOResult<UserViewModel>();

            try
            {
                var getUsersResult = await _userService.GetUsersAsync();

                if (getUsersResult.IsSuccess)
                {
                    var users = getUsersResult.Result;

                    var user = users.First(x => x.Id == _settingsService.AuthorizedUserId);

                    await Task.Delay(300);

                    if (user != null)
                    {
                        result.SetSuccess(user);
                    }
                    else
                    {
                        result.SetFailure();
                    }
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAuthorizedUserAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
