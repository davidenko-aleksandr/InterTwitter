using System;
using System.Threading.Tasks;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        public SearchPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService)
                                  : base(navigationService)
        {
            _authorizationService = authorizationService;
        }

        #region -- Public Properties --

        private string _icon = "ic_search_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private UserModel _authorizedUser;
        public UserModel AuthorizedUser
        {
            get => _authorizedUser;
            set => SetProperty(ref _authorizedUser, value);
        }

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_search_blue";

            await SetUserDataAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_search_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task SetUserDataAsync()
        {
            var result = await _authorizationService.GetAuthorizedUserAsync();
            AuthorizedUser = result.Result;
        }

        #endregion
    }
}
