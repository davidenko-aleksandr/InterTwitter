using InterTwitter.Models;
using Prism.Navigation;
using System.Windows.Input;
using InterTwitter.Helpers;
using System.Threading.Tasks;
using InterTwitter.Services.UserService;
using Plugin.Media.Abstractions;
using InterTwitter.Services.Authorization;
using InterTwitter.Validators;
using Acr.UserDialogs;
using InterTwitter.Resources;

namespace InterTwitter.ViewModels
{
    public class ChangeProfilePageViewModel : BaseViewModel
    {
        private readonly IUserService _userServcie;
        private readonly IMedia _mediaPluggin;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;

        public ChangeProfilePageViewModel(INavigationService navigatonService,
                                          IUserService userService,
                                          IMedia mediaPlugin,
                                          IAuthorizationService authorizationService,
                                          IUserDialogs userDialogs)
                                         : base(navigatonService)
        {
            _userServcie = userService;
            _mediaPluggin = mediaPlugin;
            _authorizationService = authorizationService;
            _userDialogs = _userDialogs;
        }

        #region -- Public properties --

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private string _oldPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set => SetProperty(ref _oldPassword, value);
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
        }

        public bool PasswordIsConfirmed
        {
            get => OldPassword == User.Password;
        }

        public ICommand GoBackCommand => SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        public ICommand SaveProfileCommand => SingleExecutionCommand.FromFunc(OnSaveProfileCommandAsync);

        public ICommand SetAvatarCommand => SingleExecutionCommand.FromFunc(OnSetAvatarCommand);

        public ICommand SetHeaderImageCommand => SingleExecutionCommand.FromFunc(OnSetHeaderImageCommand);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            // base.OnNavigatedTo(parameters);
            //if (parameters.TryGetValue(Constants.Navigation.User, out UserViewModel user))
            //{
            //    User = user;
            //}
            //else
            //{
            //    //wrong user
            //}
            var userResult = await _authorizationService.GetAuthorizedUserAsync();
            User = userResult.Result;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        #endregion

        #region -- Private helpers --

        private async Task OnSaveProfileCommandAsync()
        {
            var userDataIsValid = CheckDataValidity();

            if (string.IsNullOrEmpty(NewPassword) || userDataIsValid)
            {
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    User.Password = NewPassword;
                }
                else
                {
                    //password stay previous
                }
                await _userServcie.UpdateUserAsync(User);

                var parameters = new NavigationParameters()
                                {
                                    {Constants.Navigation.User, User }
                                };

                NavigationService.GoBackAsync(parameters);
            }
            else
            {
                _userDialogs.Toast(AppResource.InvalidUserDataText);
            }
        }

        private async Task OnGoBackCommandAsync()
        {            
            NavigationService.GoBackAsync();
        }

        private async Task OnSetAvatarCommand()
        {
            if (_mediaPluggin.IsPickPhotoSupported)
            {
                MediaFile file = await _mediaPluggin.PickPhotoAsync();

                if (file != null)
                {
                    User.Picture = file.Path;
                }
                else
                {
                    //CurrentPinModel == null;
                }
            }
            else
            {
                //Pick photo is not supported;
            }
        }

        private async Task OnSetHeaderImageCommand()
        {
            if (_mediaPluggin.IsPickPhotoSupported)
            {
                MediaFile file = await _mediaPluggin.PickPhotoAsync();

                if (file != null)
                {
                    User.ProfileHeaderImage = file.Path;
                }
                else
                {
                    //CurrentPinModel == null;
                }
            }
            else
            {
                //Pick photo is not supported;
            }
        }

        private bool CheckDataValidity()
        {
            var nameIsValid = !User.Name.StartsWith(" ");

            return Validator.IsMatch(NewPassword, Validator.RegexPassword) && 
                   Validator.IsMatch(User.Email, Validator.RegexEmail) && 
                   nameIsValid;
        }

        private bool CheckOldPassword()
        {
            bool isValid;
            if (OldPassword == User.Password)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
                _userDialogs.Toast(AppResource.WrongOldPassword);
            }

            return isValid;
        }
        #endregion
    }
}
