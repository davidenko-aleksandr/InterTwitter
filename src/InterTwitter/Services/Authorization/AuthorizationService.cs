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

        public AuthorizationService(
            IUserService userService,
            ISettingsService settingsService)
        {
            _userService = userService;
            _settingsService = settingsService;
        }

        #region -- IAuthorizationService implementation --

        public int AuthorizedUserId
        {
            get => _settingsService.AuthorizedUserId;
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

                    var user = users.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Password == password);

                    await Task.Delay(300);

                    if (user != null)
                    {
                        _settingsService.AuthorizedUserId = user.Id;

                        result.SetSuccess(true);
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
                    var users = getUsersResult.Result.ToList();

                    var user = new UserModel()
                    {
                        Id = users.Count,
                        Email = email,
                        Name = name,
                        Password = password,
                        Avatar = Constants.DefaultProfilePicture,
                        ProfileHeaderImage = "pic_profile_header_photo.jpg",
                    };

                    await _userService.AddUserAsync(user);

                    _settingsService.AuthorizedUserId = user.Id;

                    await Task.Delay(300);

                    result.SetSuccess(true);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(SignUpAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<bool>> CheckUserEmail(string email)
        {
            var result = new AOResult<bool>();

            try
            {
                var getUsersResult = await _userService.GetUsersAsync();

                if (getUsersResult.IsSuccess)
                {
                    var users = getUsersResult.Result;

                    var userViewModel = users.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());

                    await Task.Delay(300);

                    if (userViewModel is null)
                    {
                        result.SetSuccess(true);
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
                result.SetError($"{nameof(LogOutAsync)}: exception", "Something went wrong", ex);
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

        public async Task<AOResult<UserModel>> GetAuthorizedUserAsync()
        {
            var result = new AOResult<UserModel>();

            try
            {
                var getUsersResult = await _userService.GetUsersAsync();

                if (getUsersResult.IsSuccess)
                {
                    var users = getUsersResult.Result;

                    var user = users.FirstOrDefault(x => x.Id == _settingsService.AuthorizedUserId);

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
