using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
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
                                           IUserDialogs userDialogs)
                                           : base(navigationService)
        {
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;
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

        #endregion

    }
}
