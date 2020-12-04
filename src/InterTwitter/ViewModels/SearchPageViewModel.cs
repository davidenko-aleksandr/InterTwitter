using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using InterTwitter.Resources;
using InterTwitter.ViewModels.OwlItems;
using InterTwitter.Enums;
using Xamarin.Essentials;
using InterTwitter.Extensions;
using InterTwitter.Views;
using InterTwitter.Services.PostAction;

namespace InterTwitter.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IOwlService _owlService;
        private readonly IPostActionService _postActionService;

        public SearchPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            IOwlService owlService,
            IPostActionService postActionService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _owlService = owlService;
            _postActionService = postActionService;
        }

        #region -- Public Properties --

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private string _emptyStateText;
        public string EmptyStateText
        {
            get => _emptyStateText;
            set => SetProperty(ref _emptyStateText, value);
        }

        private string _icon = "ic_search_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private string _iconSource = "ic_search_gray";
        public string IconSource
        {
            get => _iconSource;
            set => SetProperty(ref _iconSource, value);
        }

        private string _searchBarIconSource;
        public string SearchBarIconSource
        {
            get => _searchBarIconSource;
            set => SetProperty(ref _searchBarIconSource, value);
        }

        private UserViewModel _authorizedUser;
        public UserViewModel AuthorizedUser
        {
            get => _authorizedUser;
            set => SetProperty(ref _authorizedUser, value);
        }

        private bool _isUserPictureVisible = true;
        public bool IsUserPictureVisible
        {
            get => _isUserPictureVisible;
            set => SetProperty(ref _isUserPictureVisible, value);
        }

        private bool _isClearButtonVisible;
        public bool IsClearButtonVisible
        {
            get => _isClearButtonVisible;
            set => SetProperty(ref _isClearButtonVisible, value);
        }

        private bool _isPopularThemesVisible = true;
        public bool IsPopularThemesVisible
        {
            get => _isPopularThemesVisible;
            set => SetProperty(ref _isPopularThemesVisible, value);
        }

        private bool _isFoundPostsVisible;
        public bool IsFoundPostsVisible
        {
            get => _isFoundPostsVisible;
            set => SetProperty(ref _isFoundPostsVisible, value);
        }

        private bool _isEmptyStateVisible;
        public bool IsEmptyStateVisible
        {
            get => _isEmptyStateVisible;
            set => SetProperty(ref _isEmptyStateVisible, value);
        }

        private ObservableCollection<Grouping<string, OwlViewModel>> _popularThemes;
        public ObservableCollection<Grouping<string, OwlViewModel>> PopularThemes
        {
            get => _popularThemes;
            set => SetProperty(ref _popularThemes, value);
        }

        private ObservableCollection<OwlViewModel> _foundPosts;
        public ObservableCollection<OwlViewModel> FoundPosts
        {
            get => _foundPosts;
            set => SetProperty(ref _foundPosts, value);
        }

        private States _state;
        public States State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ??= new Command(OnSearchCommand);

        public ICommand HashtagClickCommand => SingleExecutionCommand.FromFunc(OnHashtagClickCommandAsync);

        public ICommand IconClickCommand => SingleExecutionCommand.FromFunc(OnIconClickCommandAsync);

        public ICommand GoToProfilePageCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnGoToProfilePageCommandAsync);

        public ICommand OpenPostCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnOpenPostCommandAsync);

        public ICommand LikeClickCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnLikeClickCommandAsync, delayMillisec: 50);

        public ICommand BookmarkCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnBookmarkCommandAsync, delayMillisec: 50);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_search_blue";

            Connectivity.ConnectivityChanged += InternetConnectionChanged;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                State = States.Loading;
                await SetUserDataAsync();
                InitPopularThemesAsync();
            }
            else
            {
                //no internet connection
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_search_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task OnGoToProfilePageCommandAsync(OwlViewModel owl)
        {
            var navParameters = new NavigationParameters();

            navParameters.Add(Constants.Navigation.UserId, owl.Author.Id);

            await NavigationService.NavigateAsync(nameof(ProfilePage), navParameters, useModalNavigation: true, true);
        }

        private async Task OnOpenPostCommandAsync(OwlViewModel owl)
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {
                    "OwlViewModel", owl
                },
            };

            await NavigationService.NavigateAsync(nameof(PostPage), parameters, useModalNavigation: true, true);
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

        private async void InternetConnectionChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                State = States.Loading;
                await SetUserDataAsync();
                InitPopularThemesAsync();
            }
            else
            {
                State = States.NoInternet;
            }
        }

        private void OnSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                IsPopularThemesVisible = false;
                SearchBarIconSource = AppResource.LeftGreyImage;
                IsFoundPostsVisible = true;

                ShowFoundPostsAsync(SearchQuery);
            }
            else
            {
                IsPopularThemesVisible = true;
                SearchBarIconSource = AuthorizedUser.Avatar;
                IsFoundPostsVisible = false;
                IsEmptyStateVisible = false;
            }
        }

        private Task OnHashtagClickCommandAsync(object group)
        {
            SearchQuery = (group as Grouping<string, OwlViewModel>).Header;

            return Task.CompletedTask;
        }

        private Task OnIconClickCommandAsync()
        {
            if (SearchBarIconSource == AuthorizedUser.Avatar)
            {
                MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
            }
            else if (SearchBarIconSource == AppResource.LeftGreyImage)
            {
                SearchQuery = string.Empty;
            }
            else
            {
                //icon source is not set
            }

            return Task.CompletedTask;
        }

        private async Task SetUserDataAsync()
        {
            var authorizationResult = await _authorizationService.GetAuthorizedUserAsync();
            if (authorizationResult.IsSuccess)
            {
                AuthorizedUser = authorizationResult.Result.ToViewModel();
                SearchBarIconSource = AuthorizedUser.Avatar;
            }
            else
            {
                //authorization is failed
            }
        }

        private async void InitPopularThemesAsync()
        {
            var owlsResult = await _owlService.GetAllOwlsAsync();
            if (owlsResult.IsSuccess)
            {
                var posts = owlsResult.Result.Select(x => x.ToViewModel(AuthorizedUser.Id, GoToProfilePageCommand, OpenPostCommand, LikeClickCommand, BookmarkCommand));
                var allPosts = new List<OwlViewModel>();

                foreach (var post in posts)
                {
                    foreach (var hashtag in post.AllHashtags)
                    {
                        allPosts.Add(new OwlViewModel
                        {
                            CurrentHashtag = hashtag,
                        });
                    }
                }

                var groups = allPosts.GroupBy(x => x.CurrentHashtag).Select(g => new Grouping<string, OwlViewModel>(g.Key, g));
                groups = groups.OrderByDescending(x => x.Count);
                if (groups == null || !groups.Any())
                {
                    State = States.NoData;
                }
                else
                {
                    if (groups.Count() > 10)
                    {
                        PopularThemes = new ObservableCollection<Grouping<string, OwlViewModel>>(groups.Take(10));
                    }
                    else
                    {
                        PopularThemes = new ObservableCollection<Grouping<string, OwlViewModel>>(groups);
                    }

                    State = States.Normal;
                }
            }
            else
            {
                State = States.Error;
            }
        }

        private async void ShowFoundPostsAsync(string searchQuery)
        {
            var owlsResult = await _owlService.GetAllOwlsAsync(searchQuery);
            if (owlsResult.IsSuccess)
            {
                var foundPosts = new List<OwlViewModel>();
                foreach (var owl in owlsResult.Result)
                {
                    var owlVM = owl.ToViewModel(AuthorizedUser.Id, GoToProfilePageCommand, OpenPostCommand, LikeClickCommand, BookmarkCommand);
                    owlVM.SearchQuery = searchQuery;
                    foundPosts.Add(owlVM);
                }

                FoundPosts = new ObservableCollection<OwlViewModel>(foundPosts);
            }
            else
            {
                //owlsResult failed
            }

            EmptyStateText = $"\"{SearchQuery}\"";
            IsEmptyStateVisible = FoundPosts.Count == 0;
        }

        #endregion
    }
}
