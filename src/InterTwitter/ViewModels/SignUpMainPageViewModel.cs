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

        public SignUpMainPageViewModel(INavigationService navigationService,
                                       IAuthorizationService authorizationService,
                                       IKeyboardService keyboardService,
                                       IUserDialogs userDialogs)
                                       : base(navigationService)
        {
            _userDialogs = userDialogs;
            _authorizationService = authorizationService;

            keyboardService.KeyboardShown += KeyboardShown;
            keyboardService.KeyboardHidden += KeyboardHidden;
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
                    var parameters = new NavigationParameters
                    {
                        { Constants.Navigation.Name, Name },
                        { Constants.Navigation.Email, Email }
                    };

                    await NavigationService.NavigateAsync(nameof(SignUpPasswordPage), parameters);
                }
                else
                {
                    _userDialogs.Toast("Registration data error");
                }
            }
            else 
            {
                _userDialogs.Toast("No internet connection");
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

