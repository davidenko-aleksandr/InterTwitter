using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Keyboard;
using InterTwitter.Views;
using Prism.Navigation;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IKeyboardService _keyboardService;
        private readonly IUserDialogs _userDialogs;

        public LogInPageViewModel(INavigationService navigationService,
                                  IAuthorizationService authorizationService,
                                  IKeyboardService keyboardService,
                                  IUserDialogs userDialogs)
                                 : base(navigationService)
        {
            _authorizationService = authorizationService;
            _keyboardService = keyboardService;
            _userDialogs = userDialogs;

            _keyboardService.KeyboardIsShown += KeyboardIsShown;
            _keyboardService.KeyboardIsHidden += KeyboardIsHidden;

            IsButtonEnabled = false;
        }

        #region -- Public properties --

        private string _emailEntry;
        public string EmailEntry
        {
            get => _emailEntry;
            set => SetProperty(ref _emailEntry, value);
        }

        private string _passwordEntry;
        public string PasswordEntry
        {
            get => _passwordEntry;
            set => SetProperty(ref _passwordEntry, value);
        }

        private bool _isButtonEnabled;
        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set => SetProperty(ref _isButtonEnabled, value);
        }

        private bool _isKeyboardButtonVisible;
        public bool IsKeyboardButtonVisible
        {
            get => _isKeyboardButtonVisible;
            set => SetProperty(ref _isKeyboardButtonVisible, value);
        }

        private bool _isSignButtonsBlockVisible;
        public bool IsSignButtonsBlockVisible
        {
            get => _isSignButtonsBlockVisible;
            set => SetProperty(ref _isSignButtonsBlockVisible, value);
        }

        public ICommand LogInClickCommand => SingleExecutionCommand.FromFunc(OnLogInClickCommandAsync);
        public ICommand SignUpClickCommand => SingleExecutionCommand.FromFunc(OnSignUpClickCommandAsync);

        #endregion

        #region -- Private helpers --

        private async Task OnLogInClickCommandAsync()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {

                if (!string.IsNullOrWhiteSpace(EmailEntry) || !string.IsNullOrWhiteSpace(PasswordEntry))
                {
                    var result = await _authorizationService.LogInAsync(EmailEntry, PasswordEntry);

                    var isUserExist = result.Result;

                    if (isUserExist)
                    {
                        await NavigationService.NavigateAsync($"/{nameof(MenuPage)}");
                    }
                    else
                    {
                        var errorText = Resources.AppResource.WrongEmailPasswordText;
                        _userDialogs.Toast(errorText);
                    }
                }
                else
                {
                    var errorText = Resources.AppResource.EmptyEntryText;
                    _userDialogs.Toast(errorText);
                }
            }
            else
            {
                var errorText = Resources.AppResource.NoInternetText;
                _userDialogs.Toast(errorText);
            }

        }

        private async Task OnSignUpClickCommandAsync()
        {
           await NavigationService.NavigateAsync(nameof(SignUpMainPage));
        }

        private void KeyboardIsHidden(object sender, System.EventArgs e)
        {
            IsSignButtonsBlockVisible = true;
            IsKeyboardButtonVisible = false;
        }

        private void KeyboardIsShown(object sender, System.EventArgs e)
        {
            IsSignButtonsBlockVisible = false;
            IsKeyboardButtonVisible = true;
        }

        #endregion

    }
}