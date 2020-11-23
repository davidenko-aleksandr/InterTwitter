using System;
<<<<<<< HEAD
using InterTwitter.Services.Authorization;
=======
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
>>>>>>> 526f426ef49a15ba09b93a36df094470cbb483ec
using Prism.Navigation;
using InterTwitter.Helpers;
using System.Threading.Tasks;
using InterTwitter.Views.Authorization;
using System.Windows.Input;
using Xamarin.Forms;
using InterTwitter.Validators;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace InterTwitter.ViewModels.Authorization
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
<<<<<<< HEAD
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
        public ICommand LoginCommand => _loginCommand ??= SingleExecutionCommand.FromFunc(OnLOginCommand);

        #endregion

        #region --OnCommand Handlers--

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
                    // Registration data error
                }
            }
            else 
            {
                _userDialogs.Toast("no internet connection",new TimeSpan(2000));
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
            return !string.IsNullOrEmpty(Name) && Validator.IsMatch(Email, Validator.RegexEmail);
        }

        #endregion
=======

        }


>>>>>>> 526f426ef49a15ba09b93a36df094470cbb483ec
    }
}
