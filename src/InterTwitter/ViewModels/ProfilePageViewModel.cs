using System;
using System.Windows.Input;
using InterTwitter.Models;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using InterTwitter.Views;
using InterTwitter.Services.Authorization;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;

        public ProfilePageViewModel(INavigationService navigationService,
                                    IAuthorizationService authorizationService)
                                   : base(navigationService)
        {
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ICommand BackCommand => SingleExecutionCommand.FromFunc(OnBackCommandAsync);

        public ICommand ChangeProfileCommand => SingleExecutionCommand.FromFunc(OnChangeProfileCommand);
               
        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            // User = parameters.GetValue<UserViewModel>(Constants.Navigation.User);
            if (parameters.TryGetValue(Constants.Navigation.User, out UserViewModel user))
            {
                User = user;
            }
            else
            {
                var result = await _authorizationService.GetAuthorizedUserAsync();
                User = result.Result;
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
          
        }

        #endregion

        #region -- Private helpers --

        private async Task OnChangeProfileCommand()
        {
            var parameters = new NavigationParameters()
            {
                { Constants.Navigation.User, User}
            };

            await NavigationService.NavigateAsync(nameof(ChangeProfilePage), parameters);
        }

        private async Task OnBackCommandAsync()
        {
            var parameters = new NavigationParameters()
            {
                { Constants.Navigation.User, User}
            };
            NavigationService.GoBackAsync(parameters);
        }

        #endregion

    }
}
