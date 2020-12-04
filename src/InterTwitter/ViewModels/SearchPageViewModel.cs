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

namespace InterTwitter.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IOwlService _owlService;
        public SearchPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            IOwlService owlService)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _owlService = owlService;
        }

        #region -- Public Properties --

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
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
            }
        }

        private async Task OnHashtagClickCommandAsync(object group)
        {
            SearchQuery = (group as Grouping<string, OwlViewModel>).Header;
        }

        private async Task OnIconClickCommandAsync()
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
                var posts = owlsResult.Result.Select(x => x.ToViewModel(AuthorizedUser.Id, null, null, null));
                var allPosts = new List<OwlViewModel>();

                foreach (var post in posts)
                {
                    foreach (var hashtag in post.AllHashtags)
                    {
                        var newPost = new OwlViewModel
                        {
                            CurrentHashtag = hashtag
                        };

                        allPosts.Add(newPost);
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
                var collection = owlsResult.Result.Select(x => x.ToViewModel(AuthorizedUser.Id, null, null, null));
                FoundPosts = new ObservableCollection<OwlViewModel>(collection);
            }
            else
            {
                //owlsResult failed
            }
        }

        #endregion
    }
}
