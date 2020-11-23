using System;
using InterTwitter.Services.Authorization;
using Prism.Navigation;
using InterTwitter.Helpers;
using System.Threading.Tasks;
using InterTwitter.Views.Authorization;
using System.Windows.Input;
using Xamarin.Forms;
using InterTwitter.Validators;

namespace InterTwitter.ViewModels.Authorization
{
    public class SignUpMainPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;

        public SignUpMainPageViewModel(INavigationService navigationService,
                                       IAuthorizationService authorizationService)
                                      : base(navigationService)
        {
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
        public ICommand LoginCommand => _loginCommand ??= SingleExecutionCommand.FromFunc(OnLOginCommand);       

        #endregion

        #region --OnCommand Handlers--

        private async Task OnSignUpCommand()
        {
            var parameters = new NavigationParameters
            {
                { Constants.Navigation.Name, Name },
                { Constants.Navigation.Email, Email }
            };
            
            var canContinue = CanValidateData();

            if (canContinue)
            {
                await NavigationService.NavigateAsync(nameof(SignUpPasswordPage), parameters);
            }
            else
            { 
            // Registration data error
            }
        }

        private async Task OnLOginCommand()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LogInPage)}");
        }

        #endregion

        #region --Private Helpers--

        private bool CanValidateData()
        {
            return !string.IsNullOrEmpty(Name) && Validator.IsMatch(Email,Validator.RegexEmail);
        }

        #endregion
    }
}
