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
       
        public ICommand SignUpCommand => SingleExecutionCommand.FromFunc(OnSignUpCommand);
                
        public ICommand LoginCommand => SingleExecutionCommand.FromFunc(OnLoginCommand);

        #endregion

        #region -- Private helpers --

        private async Task OnSignUpCommand()
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

        private async Task OnLoginCommand()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LogInPage)}");
        }
       
        private bool ValidateData()
        {
            return !string.IsNullOrEmpty(Name) && Validator.IsMatch(Email, Validator.RegexEmail);
        }

        #endregion
    }
}

