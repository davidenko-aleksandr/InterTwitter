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
        private string _email;
        private string _name;

        public SignUpPasswordPageViewModel(INavigationService navigationService,
                                           IAuthorizationService authorizationService,
                                           IUserDialogs userDialogs,
                                           IKeyboardService keyboardService)
                                           : base(navigationService)
        {
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;

            keyboardService.KeyboardShown += KeyboardShown;
            keyboardService.KeyboardHidden += KeyboardHidden;
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

        public ICommand ConfirmPasswordCommand => SingleExecutionCommand.FromFunc(OnConfirmPasswordCommandAsync);
                
        public ICommand GoBackCommand => SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);
               
        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue<string>(Constants.Navigation.Name, out string Name) &&
                parameters.TryGetValue<string>(Constants.Navigation.Email, out string Email))
            {
                _name = Name;
                _email = Email;
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
          return  NavigationService.GoBackAsync();            
        }

        private async Task OnConfirmPasswordCommandAsync()
        {
            var isValid = ValidatePassword();

            if (isValid)
            {
                await _authorizationService.SignUpAsync(_email, _name, Password);

                var parameters = new NavigationParameters
                                {
                                    { Constants.Navigation.Email, _email }
                                };

                await NavigationService.NavigateAsync($"/{nameof(MenuPage)}");
            }
            else
            {
                _userDialogs.Toast("Passwords should match");
            }
        }

        private bool ValidatePassword()
        {
            return Validator.IsMatch(Password, Validator.RegexPassword) && Password == ConfirmPassword;
        }

        private void KeyboardHidden(object sender, System.EventArgs e)
        {
            IsSignButtonsBlockVisible = true;
            IsKeyboardButtonVisible = false;
        }

        private void KeyboardShown(object sender, System.EventArgs e)
        {
            IsSignButtonsBlockVisible = false;
            IsKeyboardButtonVisible = true;
        }

        #endregion

    }
}
