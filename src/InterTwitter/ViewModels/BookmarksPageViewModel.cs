using Acr.UserDialogs;
using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using InterTwitter.Services.PostAction;
using InterTwitter.ViewModels.OwlItems;
using InterTwitter.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class BookmarksPageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IUserDialogs _userDialogs;
        private readonly IPostActionService _postActionService;
        private readonly IAuthorizationService _authorizationService;

        public BookmarksPageViewModel(
            INavigationService navigationService,
            IOwlService owlService,
            IAuthorizationService authorizationService,
            IPostActionService postActionService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _owlService = owlService;
            _userDialogs = userDialogs;
            _postActionService = postActionService;
            _authorizationService = authorizationService;

            Icon = "ic_bookmarks_gray";
        }

        #region -- Public Properties --

        private bool _isBarIconVisible;
        public bool IsBarIconVisible
        {
            get => _isBarIconVisible;
            set => SetProperty(ref _isBarIconVisible, value);
        }

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

        private States _state;
        public States State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public ICommand MenuClickCommand => SingleExecutionCommand.FromFunc(OnMenuClickCommandAsync);
        public ICommand OpenPostCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnOpenPostCommandAsync);
        public ICommand ClearBookmarksCommand => SingleExecutionCommand.FromFunc(OnClearBookmarksCommand);
        public ICommand LikeClickCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnLikeClickCommandAsync, delayMillisec: 50);
        public ICommand BookmarkCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnBookmarkCommandAsync, delayMillisec: 50);

        #endregion

        #region -- Overrides --

        public async override void Initialize(INavigationParameters parameters)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await FillCollectionAsync();
            }
            else
            {
                BookmarksOwls = null;
            }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_bookmarks_blue";
            IsMenuVisible = false;
            IsBarIconVisible = false;

            Connectivity.ConnectivityChanged += InternetConnectionChanged;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                State = States.Loading;
                await FillCollectionAsync();
            }
            else
            {
                if (BookmarksOwls is null)
                {
                    State = States.NoInternet;
                }
                else
                {
                    // bookmarksowls is not null
                }
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_bookmarks_gray";

            Connectivity.ConnectivityChanged -= InternetConnectionChanged;
        }

        #endregion

        #region -- Private helpers --

        private async Task OnOpenPostCommandAsync(OwlViewModel owl)
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {
                    "OwlViewModel", owl
                }
            };

            await NavigationService.NavigateAsync(nameof(PostPage), parameters, useModalNavigation: true, true);
        }

        private async void InternetConnectionChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                State = States.Loading;
                await FillCollectionAsync();
            }
            else
            {
                //no internet connection
            }
        }

        private async Task OnClearBookmarksCommand()
        {
            var isConnected = Connectivity.NetworkAccess;
            if (isConnected == NetworkAccess.Internet)
            {
                BookmarksOwls = null;
                await OnMenuClickCommandAsync();
                await _postActionService.ClearUserBookmarks();
            }
            else
            {
                var errorText = Resources.AppResource.NoInternetText;
                _userDialogs.Toast(errorText);
            }
        }

        private async Task OnMenuClickCommandAsync()
        {
            IsMenuVisible = !IsMenuVisible;
        }

        private async Task FillCollectionAsync()
        {
            var owlsResult = await _owlService.GetSavedOwlsAsync();
            var userResult = await _authorizationService.GetAuthorizedUserAsync();

            if (owlsResult.IsSuccess)
            {
                var authorizedUser = userResult.Result.ToViewModel();

                BookmarksOwls = new ObservableCollection<OwlViewModel>(owlsResult.Result.Select(x => x.ToViewModel(authorizedUser.Id, OpenPostCommand, LikeClickCommand, BookmarkCommand)));
                if (BookmarksOwls is null || !BookmarksOwls.Any())
                {
                    State = States.NoData;
                }
                else
                {
                    State = States.Normal;
                    IsBarIconVisible = true;
                }
            }
            else
            {
                State = States.Error;
            }
        }

        private async Task OnLikeClickCommandAsync(OwlViewModel owl)
        {
            if (owl != null)
            {
                owl.IsLiked = !owl.IsLiked;

                owl.LikesCount = owl.IsLiked ? ++owl.LikesCount : --owl.LikesCount;

                await _postActionService.SaveActionAsync(owl.ToModel(), OwlAction.Liked);
            }
            else
            {
                //owl is null
            }
        }

        private async Task OnBookmarkCommandAsync(OwlViewModel owl)
        {
            if (owl != null)
            {
                owl.IsBookmarked = !owl.IsBookmarked;

                await _postActionService.SaveActionAsync(owl.ToModel(), OwlAction.Saved);
            }
            else
            {
                //owl is null
            }
        }

        #endregion

    }
}
