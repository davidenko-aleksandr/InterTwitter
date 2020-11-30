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
using InterTwitter.Services.Permission;
using static Xamarin.Essentials.Permissions;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class ChangeProfilePageViewModel : BaseViewModel
    {
        private readonly IUserService _userServcie;
        private readonly IMedia _mediaPluggin;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IPermissionService _permissionService;

        public string CallerPropertyName { get; private set; }

        public ChangeProfilePageViewModel(INavigationService navigatonService,
                                          IUserService userService,
                                          IMedia mediaPlugin,
                                          IAuthorizationService authorizationService,
                                          IUserDialogs userDialogs,
                                          IPermissionService permissionService)
                                         : base(navigatonService)
        {
            _userServcie = userService;
            _mediaPluggin = mediaPlugin;
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;
            _permissionService = permissionService;
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
            var userResult = await _authorizationService.GetAuthorizedUserAsync();
            User = userResult.Result;
        }

        #endregion

        #region -- Private helpers --

        private async Task OnSaveProfileCommandAsync()
        {
            var userDataIsValid = CheckUserDataValidity();
            bool newPasswordIsValid = CheckNewPasswordValidity();

            if (userDataIsValid && newPasswordIsValid)
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
                //
            }
        }

        private async Task OnGoBackCommandAsync()
        {
            NavigationService.GoBackAsync();
        }

        private async Task OnSetAvatarCommand()
        {
            CallerPropertyName = nameof(User.Picture);

            var parameters = new ActionSheetConfig();
            parameters.Add(AppResource.TakeCameraPicture, TakeCameraPicture, null);
            parameters.Add(AppResource.TakeGalleryPicture, TakeGalleryPicture, null);
            parameters.SetCancel(AppResource.CancelText, null, null);    
            
            _userDialogs.ActionSheet(parameters);

            #region Cut
            //if (_mediaPluggin.IsPickPhotoSupported)
            //{
            //    MediaFile file = await _mediaPluggin.PickPhotoAsync();

            //    if (file != null)
            //    {
            //        User.Picture = file.Path;
            //    }
            //    else
            //    {
            //        //CurrentPinModel == null;
            //    }
            //}
            //else
            //{
            //    //Pick photo is not supported;
            //}
            #endregion

        }

        private async Task OnSetHeaderImageCommand()
        {
            CallerPropertyName = nameof(User.ProfileHeaderImage);

            var parameters = new ActionSheetConfig();
            parameters.Add(AppResource.TakeCameraPicture, TakeCameraPicture, null);
            parameters.Add(AppResource.TakeGalleryPicture, TakeGalleryPicture, null);
            parameters.SetCancel(AppResource.CancelText, null, null);

            _userDialogs.ActionSheet(parameters);
        }

        private bool CheckNewPasswordValidity()
        {
            return string.IsNullOrEmpty(NewPassword) || Validator.IsMatch(NewPassword, Validator.RegexPassword);
        }

        private bool CheckUserDataValidity()
        {
            bool isValid;
            if (OldPassword != User.Password)
            {
                _userDialogs.Toast(AppResource.WrongEmailPasswordText);
                isValid = false;
            }
            else if (!Validator.IsMatch(User.Name, Validator.RegexName))
            {
                _userDialogs.Toast(AppResource.WrongNameText);
                isValid = false;
            }
            else if (!Validator.IsMatch(User.Email, Validator.RegexEmail))
            {
                _userDialogs.Toast(AppResource.WrongEmailPasswordText);
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private async void TakeCameraPicture()
        {
            var cameraStaus =  await _permissionService.CheckPermissionAsync<Camera>();

            if (cameraStaus == PermissionStatus.Granted)
            {
                if (_mediaPluggin.IsTakePhotoSupported && _mediaPluggin.IsCameraAvailable)
                {
                    var options = new StoreCameraMediaOptions();
                    options.SaveToAlbum = true;
                    options.PhotoSize = PhotoSize.Full;
                    
                    MediaFile file = await _mediaPluggin.TakePhotoAsync(options);

                    if (file != null)
                    {
                        SetPicture(CallerPropertyName, file.Path);
                    }
                }
            }
            else
            {
                await _userDialogs.AlertAsync(AppResource.CameraPermisionWarning, okText: AppResource.OkText);
                await Task.Delay(3000);
                await _permissionService.RequestPermissionAsync<Camera>();

            }
        }

        private async void TakeGalleryPicture()
        {
            if (_mediaPluggin.IsPickPhotoSupported)
            {
                MediaFile file = await _mediaPluggin.PickPhotoAsync();

                if (file != null)
                {
                    SetPicture(CallerPropertyName, file.Path);
                }
                else
                {
                    //Currentfile == null;
                }
            }
            else
            {
                //Pick photo is not supported;
            }
        }

        private void SetPicture(string propertyName, string path)
        {
            switch (propertyName)
            {
                case nameof(User.ProfileHeaderImage):
                    {
                        User.ProfileHeaderImage = path;
                        break;
                    }
                case nameof(User.Picture):
                    {
                        User.Picture = path;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
      
        #endregion
    }
}
