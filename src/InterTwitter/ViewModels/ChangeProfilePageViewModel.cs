using InterTwitter.Models;
using Prism.Navigation;
using System.Windows.Input;
using InterTwitter.Helpers;
using System.Threading.Tasks;
using InterTwitter.Services.UserService;
using Plugin.Media.Abstractions;

namespace InterTwitter.ViewModels
{
    public class ChangeProfilePageViewModel : BaseViewModel
    {
        private readonly IUserService _userServcie;
        private readonly IMedia _mediaPluggin;

        public ChangeProfilePageViewModel(INavigationService navigatonService,
                                          IUserService userService,
                                          IMedia mediaPlugin) 
                                         : base(navigatonService)
        {
            _userServcie = userService;
            _mediaPluggin = mediaPlugin;
        }

        #region -- Public properties --

        private UserModel _user;
        public UserModel User
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            // base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue(Constants.Navigation.User, out UserModel user))
            {
                User = user;
            }
            else
            { 
            //wrong user
            }
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
