using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Keyboard;
using InterTwitter.Validators;
using InterTwitter.Views;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class SignUpPasswordPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;
        private readonly IKeyboardService _keyboardService;
        private string _email;
        private string _name;

        public SignUpPasswordPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            IUserDialogs userDialogs,
            IKeyboardService keyboardService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;
            _keyboardService = keyboardService;

            _keyboardService.KeyboardShown += KeyboardShown;
            _keyboardService.KeyboardHidden += KeyboardHidden;
        }

        #region -- Public Properties --

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
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

        public ICommand ConfirmPasswordCommand => SingleExecutionCommand.FromFunc(OnConfirmCommandAsync);

        public ICommand GoBackCommand => SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue<string>(Constants.Navigation.Name, out string name) &&
                parameters.TryGetValue<string>(Constants.Navigation.Email, out string email))
            {
                _name = name;
                _email = email;
            }
            else
            {
                Debug.WriteLine("value error");
            }
        }

        #endregion

        #region -- Private helpers --

        private Task OnGoBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }

        private async Task OnConfirmCommandAsync()
        {
            var isValid = ValidatePassword();
            if (isValid)
            {
                var signUpResult = await _authorizationService.SignUpAsync(_email, _name, Password);
                if (signUpResult.IsSuccess)
                {
                    await NavigationService.NavigateAsync($"/{nameof(MenuPage)}");
                }
                else
                {
                    var errorText = Resources.AppResource.RandomError;
                    _userDialogs.Toast(errorText);
                }
            }
            else
            {
                //entry not valid
            }
        }

        private bool ValidatePassword()
        {
            return Validator.IsMatch(Password, Validator.RegexPassword) && Password == ConfirmPassword;
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
