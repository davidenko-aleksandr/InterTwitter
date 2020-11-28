using InterTwitter.Models;
using Prism.Navigation;
using System.Windows.Input;
using InterTwitter.Helpers;
using System.Threading.Tasks;
using InterTwitter.Services.UserService;
using Plugin.Media.Abstractions;
using InterTwitter.Services.Authorization;

namespace InterTwitter.ViewModels
{
    public class ChangeProfilePageViewModel : BaseViewModel
    {
        private readonly IUserService _userServcie;
        private readonly IMedia _mediaPluggin;
        private readonly IAuthorizationService _authorizationService;

        public ChangeProfilePageViewModel(INavigationService navigatonService,
                                          IUserService userService,
                                          IMedia mediaPlugin,
                                          IAuthorizationService authorizationService)
                                         : base(navigatonService)
        {
            _userServcie = userService;
            _mediaPluggin = mediaPlugin;
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set => SetProperty(ref _newPassword, value);
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

        #endregion

        #region -- Private helpers --

        private async Task OnSaveProfileCommandAsync()
        {
            await _userServcie.UpdateUserAsync(User);
            NavigationService.GoBackAsync();
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
                    //"CurrentPinModel == null";
                }
            }
            else
            {
                //"Pick photo is not supported";
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
                    //"CurrentPinModel == null";
                }
            }
            else
            {
                //"Pick photo is not supported";
            }
        }

        #endregion
    }
}
