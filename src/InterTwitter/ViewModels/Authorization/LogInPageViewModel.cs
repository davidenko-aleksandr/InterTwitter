using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
using InterTwitter.Views;
using InterTwitter.Views.Authorization;
using Prism.Navigation;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels.Authorization
{
    public class LogInPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;

        public LogInPageViewModel(INavigationService navigationService,
                                  IAuthorizationService authorizationService,
                                  IUserDialogs userDialogs)
                                 : base(navigationService)
        {
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;

            IsButtonEnabled = false;
        }

        #region -- Public properties --

        private string _emailEntry;
        public string EmailEntry
        {
            get => _emailEntry;
            set => SetProperty(ref _emailEntry, value);
        }

<<<<<<< HEAD
        public override void Initialize(INavigationParameters parameters)
=======
        private string _passwordEntry;
        public string PasswordEntry
>>>>>>> 786f357b32fe15819f7569d2b6298ba118801c34
        {
            get => _passwordEntry;
            set => SetProperty(ref _passwordEntry, value);
        }

        private bool _isButtonEnabled;
        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set => SetProperty(ref _isButtonEnabled, value);
        }

        public ICommand LogInClickCommand => SingleExecutionCommand.FromFunc(OnLogInClickCommandAsync);
        public ICommand SignUpClickCommand => SingleExecutionCommand.FromFunc(OnSignUpClickCommandAsync);

        #endregion

        #region -- Private helpers --

        private async Task OnLogInClickCommandAsync()
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
                        await NavigationService.NavigateAsync($"/{nameof(MenuPage)}");
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

        private async Task OnSignUpClickCommandAsync()
        {
           await NavigationService.NavigateAsync(nameof(SignUpMainPage));
        }

        #endregion

    }
}