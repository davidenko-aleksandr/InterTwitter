using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterTwitter.Services.Owl;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using InterTwitter.ViewModels.OwlItems;
using InterTwitter.Views;
using Xamarin.Essentials;
using Acr.UserDialogs;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.PostAction;
using InterTwitter.Enums;
using System.Linq;
using InterTwitter.Extensions;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IUserDialogs _userDialogs;
        private readonly IAuthorizationService _authorizationService;
        private readonly IPostActionService _postActionService;

        public HomePageViewModel(
            INavigationService navigationService,
            IOwlService owlService,
            IUserDialogs userDialogs,
            IAuthorizationService authorizationService,
            IPostActionService postActionService)
            : base(navigationService)
        {
            _owlService = owlService;
            _userDialogs = userDialogs;
            _authorizationService = authorizationService;
            _postActionService = postActionService;
        }

        #region -- Public properties --

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        private string _icon = "ic_home_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private UserViewModel _authorizedUser;
        public UserViewModel AuthorizedUser
        {
            get => _authorizedUser;
            set => SetProperty(ref _authorizedUser, value);
        }

        private OwlViewModel _selectedItem;
        public OwlViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private States _state;
        public States State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }
        
        public ICommand OpenMenuCommand => SingleExecutionCommand.FromFunc(OnOpenMenuCommandAsync);

        public ICommand OpenPostCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnOpenPostCommandAsync);

        public ICommand AddPostCommand => SingleExecutionCommand.FromFunc(OnAddPostCommandAsync);

        public ICommand LikeClickCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnLikeClickCommandAsync);

        public ICommand BookmarkCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnBookmarkCommandAsync);

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
                Owls = null;
            }

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_home_blue";

            Connectivity.ConnectivityChanged += InternetConnectionChanged;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                State = States.Loading;
                await FillCollectionAsync();
            }
            else
            {
                if (Owls is null)
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
            Icon = "ic_home_gray";

            Connectivity.ConnectivityChanged -= InternetConnectionChanged;
        }

        #endregion

        #region -- Private helpers --


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

        private async Task FillCollectionAsync()
        {
            var owlsResult = await _owlService.GetAllOwlsAsync();
            var userResult = await _authorizationService.GetAuthorizedUserAsync();

            if (owlsResult.IsSuccess && userResult.IsSuccess)
            {
                AuthorizedUser = userResult.Result.ToViewModel();

                Owls = new ObservableCollection<OwlViewModel>(owlsResult.Result.Select(x => x.ToViewModel(AuthorizedUser.Id, OpenPostCommand, LikeClickCommand, BookmarkCommand)));
                if (Owls is null || !Owls.Any())
                {
                    State = States.NoData;
                }
                else
                {
                    State = States.Normal;
                }

            }
            else
            {
                State = States.Error;
            }
        }

        private async Task OnOpenMenuCommandAsync()
        {
            MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
        }

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

        private async Task OnAddPostCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(AddPostPage), new NavigationParameters(), useModalNavigation: true, true);
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
            if (owl is not null)
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