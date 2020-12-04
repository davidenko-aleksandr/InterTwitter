using System.Windows.Input;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using InterTwitter.Views;
using InterTwitter.Services.Authorization;
using System.Collections.ObjectModel;
using InterTwitter.Services.Owl;
using InterTwitter.Services.UserService;
using InterTwitter.ViewModels.ProfilePageItems;
using InterTwitter.ViewModels.OwlItems;
using System.Collections.Generic;
using System.Linq;
using InterTwitter.Extensions;
using InterTwitter.Models;
using InterTwitter.Services.PostAction;
using InterTwitter.Enums;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IOwlService _owlService;
        private readonly IUserService _userService;
        private readonly IPostActionService _postActionService;

        public ProfilePageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            IOwlService owlService,
            IUserService userService,
            IPostActionService postActionService)
           : base(navigationService)
        {
            _authorizationService = authorizationService;
            _owlService = owlService;
            _userService = userService;
            _postActionService = postActionService;
        }

        #region -- Public properties --

        private bool _isMuted;
        public bool IsMuted
        {
            get => _isMuted;
            set => SetProperty(ref _isMuted, value);
        }

        private bool _isVisibleToBlackListConfirm;
        public bool IsVisibleToBlackListConfirm
        {
            get => _isVisibleToBlackListConfirm;
            set => SetProperty(ref _isVisibleToBlackListConfirm, value);
        }

        private bool _isVisibleFromBlackListConfirm;
        public bool IsVisibleFromBlackListConfirm
        {
            get => _isVisibleFromBlackListConfirm;
            set => SetProperty(ref _isVisibleFromBlackListConfirm, value);
        }

        private bool _isBackFrameIsVisible;
        public bool IsBackFrameIsVisible
        {
            get => _isBackFrameIsVisible;
            set => SetProperty(ref _isBackFrameIsVisible, value);
        }

        private bool _isMenuVisible;
        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set => SetProperty(ref _isMenuVisible, value);
        }

        private bool _isSecondaryMenuVisible;
        public bool IsSecondaryMenuVisible
        {
            get => _isSecondaryMenuVisible;
            set => SetProperty(ref _isSecondaryMenuVisible, value);
        }

        private bool _isAuthorized;
        public bool IsAuthorized
        {
            get => _isAuthorized;
            set => SetProperty(ref _isAuthorized, value);
        }

        private ObservableCollection<ProfilePageItemViewModel> _tabs;
        public ObservableCollection<ProfilePageItemViewModel> Tabs
        {
            get => _tabs;
            set => SetProperty(ref _tabs, value);
        }

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        private ObservableCollection<OwlViewModel> _likedOwls;
        public ObservableCollection<OwlViewModel> LikedOwls
        {
            get => _likedOwls;
            set => SetProperty(ref _likedOwls, value);
        }

        public ICommand GoToProfilePageCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnGoToProfilePageCommandAsync);

        public ICommand BackCommand => SingleExecutionCommand.FromFunc(OnBackCommandAsync);

        public ICommand ChangeProfileCommand => SingleExecutionCommand.FromFunc(OnChangeProfileCommandAsync);

        public ICommand CancellCommand => SingleExecutionCommand.FromFunc(OnCancelCommandAsync);

        public ICommand OpenDialogCommand => SingleExecutionCommand.FromFunc(OnOpenDialogCommand);

        public ICommand MenuClickCommand => SingleExecutionCommand.FromFunc(OnMenuClickCommandAsync);

        public ICommand OpenPostCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnOpenPostCommandAsync);

        public ICommand LikeClickCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnLikeClickCommandAsync, delayMillisec: 50);

        public ICommand BookmarkCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnBookmarkCommandAsync, delayMillisec: 50);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(Constants.Navigation.User, out UserViewModel user))
            {
                User = user;
            }
            else if (parameters.TryGetValue(Constants.Navigation.UserId, out int userId))
            {
                var userAOResult = await _userService.GetUserAsync(userId);
                User = new UserViewModel(userAOResult.Result);
            }
            else
            {
                var result = await _authorizationService.GetAuthorizedUserAsync();
                User = new UserViewModel(result.Result);
            }

            await SetDataAsync();
            InitTabs();
        }

        #endregion

        #region -- Private helpers --

        private async Task OnGoToProfilePageCommandAsync(OwlViewModel owl)
        {
            var navParameters = new NavigationParameters();

            navParameters.Add(Constants.Navigation.User, owl.Author.Id);

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

        private Task OnOpenDialogCommand()
        {
            if (IsMuted)
            {
                IsVisibleToBlackListConfirm = true;
            }
            else
            {
                IsVisibleFromBlackListConfirm = true;
            }

            return Task.CompletedTask;
        }

        private Task OnMenuClickCommandAsync()
        {
            if (IsBackFrameIsVisible)
            {
                IsBackFrameIsVisible = false;
                IsMenuVisible = false;
                IsSecondaryMenuVisible = false;
            }
            else if (IsAuthorized)
            {
                IsBackFrameIsVisible = true;
                IsMenuVisible = true;
            }
            else
            {
                IsBackFrameIsVisible = true;
                IsSecondaryMenuVisible = true;
            }

            return Task.CompletedTask;
        }

        private Task OnChangeProfileCommandAsync()
        {
            var parameters = new NavigationParameters();

            parameters.Add(Constants.Navigation.User, User);

            return NavigationService.NavigateAsync(nameof(ChangeProfilePage), parameters);
        }

        private Task OnBackCommandAsync()
        {
            var parameters = new NavigationParameters();
            parameters.Add(Constants.Navigation.User, User);

            return NavigationService.GoBackAsync(parameters);
        }

        private void InitTabs()
        {
            var tabs = new List<ProfilePageItemViewModel>()
            {
                { new PostsViewModel("Posts", Owls) },
                { new LikesViewModel("Likes", LikedOwls) },
            };

            Tabs = new ObservableCollection<ProfilePageItemViewModel>(tabs);
        }

        private async Task SetDataAsync()
        {
            IsAuthorized = User.Id == _authorizationService.AuthorizedUserId;
            var owlAOResult = _owlService.GetAuthorOwlsAsync(User.Id);
            var likedOwlsAOresult = _owlService.GetLikedOwlsAsync(User.Id);

            await Task.WhenAll(owlAOResult, likedOwlsAOresult);

            var owlList = ConvertToList(owlAOResult);
            var likedList = ConvertToList(likedOwlsAOresult);

            Owls = new ObservableCollection<OwlViewModel>(owlList);
            LikedOwls = new ObservableCollection<OwlViewModel>(likedList);
        }

        private Task OnCancelCommandAsync()
        {
            IsVisibleToBlackListConfirm = false;
            IsVisibleFromBlackListConfirm = false;

            return Task.CompletedTask;
        }

        private List<OwlViewModel> ConvertToList(Task<AOResult<IEnumerable<OwlModel>>> result)
        {
            var owlResult = result.Result.Result;
            return owlResult.Select(owl => owl.ToViewModel(User.Id, GoToProfilePageCommand, OpenPostCommand, LikeClickCommand, BookmarkCommand)).ToList();
        }

        #endregion

    }
}
