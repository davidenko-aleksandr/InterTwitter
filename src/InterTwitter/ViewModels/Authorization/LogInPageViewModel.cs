using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
using InterTwitter.Views;
using InterTwitter.Views.Authorization;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels.Authorization
{
    public class LogInPageViewModel : BaseViewModel
    {
        public LogInPageViewModel(INavigationService navigationService)
                                 : base(navigationService)
        {

        }

        #region -- Public properties --

        private string _emailEntry;
        public string EmailEntry
        {
            get => _emailEntry;
            set => SetProperty(ref _emailEntry, value);
        }

        private string _passwordEntry;
        public string PasswordEntry
        {
            get => _passwordEntry;
            set => SetProperty(ref _passwordEntry, value);
        }

        public ICommand LogInClickCommand => SingleExecutionCommand.FromFunc(OnLogInClickCommandAsync);
        public ICommand SignUpClickCommand => SingleExecutionCommand.FromFunc(OnSignUpClickCommandAsync);

        #endregion

        #region -- Private helpers --

        private async Task OnLogInClickCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(MenuPage));
        }

        private async Task OnSignUpClickCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(SignUpMainPage));
        }

        #endregion

    }
}