using System;
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
using System.Diagnostics;

namespace InterTwitter.ViewModels
{
    public class SignUpMainPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;

        public SignUpMainPageViewModel(INavigationService navigationService,
                                       IAuthorizationService authorizationService,
                                       IUserDialogs userDialogs)
                                       : base(navigationService)
        {
            _userDialogs = userDialogs;
            _authorizationService = authorizationService;
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

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= SingleExecutionCommand.FromFunc(OnSignUpCommand);

        private ICommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ??= SingleExecutionCommand.FromFunc(OnLoginCommand);

        #endregion

        #region -- Private helpers --

        private async Task OnSignUpCommand()
        {
            var isConnected = Connectivity.NetworkAccess;
            if (isConnected == NetworkAccess.Internet)
            {
                var canContinue = CanValidateData();

                if (canContinue)
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
                    Debug.WriteLine("Registration data error");
                }
            }
            else 
            {
                _userDialogs.Toast("No internet connection",new TimeSpan(3000));
            }
        }

        private async Task OnLoginCommand()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LogInPage)}");
        }
       
        private bool CanValidateData()
        {
            return !string.IsNullOrEmpty(Name) && Validator.IsMatch(Email, Validator.RegexEmail);
        }

        #endregion
    }
}

