using System;
using System.Windows.Input;
using InterTwitter.Models;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using InterTwitter.Views;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public ProfilePageViewModel(INavigationService navigationService)
                                   : base(navigationService)
        {
        }

        #region -- Public properties --

        private UserModel _user;
        public UserModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        public ICommand ChangeProfileCommand => SingleExecutionCommand.FromFunc(OnChangeProfileCommand);
               
        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(Constants.Navigation.User, out UserModel user))
            {
                User = user;
            }
            else
            { 
            //Wrong user
            }

        }

        #endregion

        #region -- Private helpers --

        private async Task OnChangeProfileCommand()
        {
            var parameers = new NavigationParameters()
            {
                { Constants.Navigation.User, User}
            };

           await NavigationService.NavigateAsync(nameof(ChangeProfilePage), parameers);
        }

        #endregion

    }
}
