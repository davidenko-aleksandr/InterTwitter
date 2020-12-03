using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.OwlItems;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class BookmarksPageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IUserDialogs _userDialogs;

        public BookmarksPageViewModel(
            INavigationService navigationService,
            IOwlService owlService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _owlService = owlService;
            _userDialogs = userDialogs;
            Icon = "ic_bookmarks_gray";
        }

        #region -- Public Properties --

        private string _icon;
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private ObservableCollection<OwlViewModel> _bookmarksOwls;
        public ObservableCollection<OwlViewModel> BookmarksOwls
        {
            get => _bookmarksOwls;
            set => SetProperty(ref _bookmarksOwls, value);
        }

        private bool _isMenuVisible;
        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set => SetProperty(ref _isMenuVisible, value);
        }


        public ICommand MenuClickCommand => SingleExecutionCommand.FromFunc(OnMenuClickCommandAsync);
        public ICommand ClearBookmarksCommand => SingleExecutionCommand.FromFunc(OnClearBookmarksCommand);

        #endregion

        #region -- Overrides --

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_bookmarks_blue";
            IsMenuVisible = false;

            await FillCollectionAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_bookmarks_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task OnClearBookmarksCommand()
        {
            var isConnected = Connectivity.NetworkAccess;
            if (isConnected == NetworkAccess.Internet)
            {
                BookmarksOwls = null;
                await OnMenuClickCommandAsync();
                await _owlService.ClearUserBookmarks();
            }
            else
            {
                var errorText = Resources.AppResource.NoInternetText;
                _userDialogs.Toast(errorText);
            }

        }

        private  Task OnMenuClickCommandAsync()
        {
            IsMenuVisible = !IsMenuVisible;

            return Task.CompletedTask;
        }

        private async Task FillCollectionAsync()
        {
            var isConnected = Connectivity.NetworkAccess;
            if (isConnected == NetworkAccess.Internet)
            {
                var owlsResult = await _owlService.GetSavedOwlsAsync();
                if (owlsResult.IsSuccess)
                {
                    BookmarksOwls = new ObservableCollection<OwlViewModel>(owlsResult.Result);
                }
                else
                {
                    var errorText = Resources.AppResource.RandomError;
                    _userDialogs.Toast(errorText);
                }

            }
            else
            {
                var errorText = Resources.AppResource.NoInternetText;
                _userDialogs.Toast(errorText);
            }

        }

        #endregion

    }
}
