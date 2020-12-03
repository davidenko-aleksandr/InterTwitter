using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Keyboard;
using InterTwitter.Views;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class LogInPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IKeyboardService _keyboardService;

        public LogInPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            IKeyboardService keyboardService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;
            _keyboardService = keyboardService;

            _keyboardService.KeyboardShown += KeyboardShown;
            _keyboardService.KeyboardHidden += KeyboardHidden;
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

        private bool _isKeyboardButtonVisible;
        public bool IsKeyboardButtonVisible
        {
            get => _isKeyboardButtonVisible;
            set => SetProperty(ref _isKeyboardButtonVisible, value);
        }

        private bool _isSignButtonsBlockVisible = true;
        public bool IsSignButtonsBlockVisible
        {
            get => _isSignButtonsBlockVisible;
            set => SetProperty(ref _isSignButtonsBlockVisible, value);
        }

        private double _keyboardButtonTranslationY;
        public double KeyboardButtonTranslationY
        {
            get => _keyboardButtonTranslationY;
            set => SetProperty(ref _keyboardButtonTranslationY, value);
        }

        public ICommand LogInCommand => SingleExecutionCommand.FromFunc(OnLogInCommandAsync);
        public ICommand SignUpCommand => SingleExecutionCommand.FromFunc(OnSignUpCommandAsync);

        #endregion

        #region -- Private helpers --

        private async Task OnLogInCommandAsync()
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
                        await NavigationService.NavigateAsync($"/{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(MainTabbedPage)}");
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

        private Task OnSignUpCommandAsync()
        {
            return NavigationService.NavigateAsync(nameof(SignUpMainPage));
        }

        private void KeyboardHidden(object sender, System.EventArgs e)
        {
            IsKeyboardButtonVisible = false;
            IsSignButtonsBlockVisible = true;

            KeyboardButtonTranslationY = 0.0d;
        }

        private void KeyboardShown(object sender, System.EventArgs e)
        {
            IsSignButtonsBlockVisible = false;
            IsKeyboardButtonVisible = true;

            var keyboardHeight = _keyboardService.FrameHeight;

            KeyboardButtonTranslationY = keyboardHeight != 0.0f ? -keyboardHeight : KeyboardButtonTranslationY;

        }

        #endregion
    }
}