using InterTwitter.Services.Authorization;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
using Prism.Navigation;
using Xamarin.Forms;
using InterTwitter.Validators;
using Xamarin.Essentials;
using Acr.UserDialogs;
using InterTwitter.Views;
using InterTwitter.Services.Keyboard;
using System.Text.RegularExpressions;

namespace InterTwitter.ViewModels
{
    public class SignUpMainPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IKeyboardService _keyboardService;

        public SignUpMainPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            IKeyboardService keyboardService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _authorizationService = authorizationService;
            _keyboardService = keyboardService;

            _keyboardService.KeyboardShown += KeyboardShown;
            _keyboardService.KeyboardHidden += KeyboardHidden;
        }

        #region --Public properties--

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
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

        public ICommand SignUpCommand => SingleExecutionCommand.FromFunc(OnSignUpCommandAsync);
                
        public ICommand LogInCommand => SingleExecutionCommand.FromFunc(OnLoginCommandAsync);

        #endregion

        #region -- Private helpers --

        private async Task OnSignUpCommandAsync()
        {
            var isConnected = Connectivity.NetworkAccess;
            if (isConnected == NetworkAccess.Internet)
            {
                var isValid = ValidateData();
                if (isValid)
                {
                    var checkResult = await _authorizationService.CheckUserEmail(Email);
                    if (checkResult.IsSuccess)
                    {
                        var parameters = new NavigationParameters
                        {
                            { Constants.Navigation.Name, Name },
                            { Constants.Navigation.Email, Email }
                        };

                        await NavigationService.NavigateAsync(nameof(SignUpPasswordPage), parameters);
                    }
                    else
                    {
                        var errorText = Resources.AppResource.ExistEmailError;
                        _userDialogs.Toast(errorText);
                    }

                }
                else
                {
                    //isValid is false
                }

            }
            else 
            {
                var errorText = Resources.AppResource.NoInternetText;
                _userDialogs.Toast(errorText);
            }
        }

        private async Task OnLoginCommandAsync()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LogInPage)}");
        }
       
        private bool ValidateData()
        {
            return Validator.IsMatch(Name, Validator.RegexName) && Validator.IsMatch(Email, Validator.RegexEmail, RegexOptions.IgnoreCase);
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

