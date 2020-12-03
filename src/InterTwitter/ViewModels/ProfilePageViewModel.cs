using System;
using System.Windows.Input;
using InterTwitter.Models;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using InterTwitter.Views;
using InterTwitter.Services.Authorization;
using System.Collections.ObjectModel;
using InterTwitter.Services.Owl;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IOwlService _owlService;

        public ProfilePageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            IOwlService owlService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _owlService = owlService;
        }

        #region -- Public properties --

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private ObservableCollection<OwlModel> _owls;
        public ObservableCollection<OwlModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        public ICommand BackCommand => SingleExecutionCommand.FromFunc(OnBackCommandAsync);

        public ICommand ChangeProfileCommand => SingleExecutionCommand.FromFunc(OnChangeProfileCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(Constants.Navigation.User, out UserViewModel user))
            {
                User = user;
            }
            else
            {
                await SetAuthorizedUserAsync();
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task SetAuthorizedUserAsync()
        {
            var result = await _authorizationService.GetAuthorizedUserAsync();

            if (result.IsSuccess)
            {
                var userResult = result.Result;

                if (userResult != null)
                {
                    User = userResult;
                }
                else
                {
                    //userResult was null
                }
            }
            else
            {
                //result is failed
            }
        }

        private async Task OnChangeProfileCommandAsync()
        {
            var parameters = new NavigationParameters();

            parameters.Add(Constants.Navigation.User, User);

            await NavigationService.NavigateAsync(nameof(ChangeProfilePage), parameters);
        }

        private async Task OnBackCommandAsync()
        {
            var parameters = new NavigationParameters();
            parameters.Add(Constants.Navigation.User, User);

            await NavigationService.GoBackAsync(parameters);
        }

        #endregion

    }
}
