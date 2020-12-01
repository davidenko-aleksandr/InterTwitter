using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.OwlItems;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class BookmarksPageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IUserDialogs _userDialogs;
        private readonly IAuthorizationService _authorizationService;

        public BookmarksPageViewModel(
                                      INavigationService navigationService,
                                      IOwlService owlService,
                                      IUserDialogs userDialogs,
                                      IAuthorizationService authorizationService) : base(navigationService)
        {
            _owlService = owlService;
            _userDialogs = userDialogs;
            _authorizationService = authorizationService;
        }

        #region -- Public Properties --

        private string _icon = "ic_bookmarks_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        public ICommand MenuClickCommand => SingleExecutionCommand.FromFunc(OnMenuClickCommandAsync);

        #endregion

        #region -- Overrides --

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_bookmarks_blue";

            var isConnected = Connectivity.NetworkAccess;

            if (isConnected == NetworkAccess.Internet)
            {
                await FillCollectionAsync();
            }
            else
            {
                var errorText = Resources.AppResource.NoInternetText;
                _userDialogs.Toast(errorText);
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_bookmarks_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task OnMenuClickCommandAsync()
        {
            
        }

        private async Task FillCollectionAsync()
        {
            var owlsResult = await _owlService.GetSavedOwlsAsync();
            if (owlsResult.IsSuccess)
            {
                Owls = new ObservableCollection<OwlViewModel>(owlsResult.Result);
            }
            else
            {
                var errorText = Resources.AppResource.RandomError;
                _userDialogs.Toast(errorText);
            }
        }

        #endregion

    }
}
