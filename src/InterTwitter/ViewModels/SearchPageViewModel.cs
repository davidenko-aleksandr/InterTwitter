using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.HomePageItems;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using InterTwitter.Resources;

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

        private UserModel _authorizedUser;
        public UserModel AuthorizedUser
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

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ??= new Command(OnSearchCommand);

        public ICommand HashtagClickCommand => SingleExecutionCommand.FromFunc(OnHashtagClickCommandAsync);

        public ICommand ClearCommand => SingleExecutionCommand.FromFunc(OnClearCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_search_blue";

            await SetUserDataAsync();
            InitPopularThemesAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_search_gray";
        }

        #endregion

        #region -- Private helpers --

        private void OnSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                IsUserPictureVisible = false;
                IsClearButtonVisible = true;
                IsFoundPostsVisible = true;

                ShowFoundPostsAsync(SearchQuery);
            }
            else
            {
                IsUserPictureVisible = true;
                IsClearButtonVisible = false;
                IsFoundPostsVisible = false;
            }
        }

        private async Task OnHashtagClickCommandAsync(object group)
        {
            SearchQuery = (group as Grouping<string, OwlViewModel>).Header;
        }

        private async Task OnClearCommandAsync()
        {
            SearchQuery = string.Empty;
        }

        private async Task SetUserDataAsync()
        {
            var result = await _authorizationService.GetAuthorizedUserAsync();
            AuthorizedUser = result.Result;
        }

        private async void InitPopularThemesAsync()
        {
            var answer = await _owlService.GetAllOwlsAsync();
            var posts = answer.Result;
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

            if (groups.Count() > 10)
            {
                PopularThemes = new ObservableCollection<Grouping<string, OwlViewModel>>(groups.Take(10));
            }
            else
            {
                PopularThemes = new ObservableCollection<Grouping<string, OwlViewModel>>(groups);
            }
        }

        private async void ShowFoundPostsAsync(string searchQuery)
        {
            var answer = await _owlService.GetAllOwlsAsync(searchQuery);
            FoundPosts = new ObservableCollection<OwlViewModel>(answer.Result);
        }

        #endregion
    }
}
