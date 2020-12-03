using System.Windows.Input;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using InterTwitter.Views;
using InterTwitter.Services.Authorization;
using System.Collections.ObjectModel;
using InterTwitter.Services.Owl;
using System.Collections.Generic;
using InterTwitter.ViewModels.ProfilePageItems;
using InterTwitter.Services.UserService;
using InterTwitter.ViewModels.OwlItems;

namespace InterTwitter.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IOwlService _owlService;
        private readonly IUserService _userService;

        public ProfilePageViewModel(INavigationService navigationService,
                                    IAuthorizationService authorizationService,
                                    IOwlService owlService,
                                    IUserService userService)
                                   : base(navigationService)
        {
            _authorizationService = authorizationService;
            _owlService = owlService;
            _userService = userService;
        }

        #region -- Public properties --

        private bool _isMuted;
        public bool IsMuted
        {
            get => _isMuted;
            set => SetProperty(ref _isMuted, value);
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

        private ObservableCollection<PofilePageItemViewModel> _tabs;
        public ObservableCollection<PofilePageItemViewModel> Tabs
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

        public ICommand BackCommand => SingleExecutionCommand.FromFunc(OnBackCommandAsync);

        public ICommand ChangeProfileCommand => SingleExecutionCommand.FromFunc(OnChangeProfileCommandAsync);

        public ICommand MenuClickCommand => SingleExecutionCommand.FromFunc(OnMenuClickCommandAsync);
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
                User = result.Result;
            }
            await SetDataAsync();
            InitTabs();
        }

        #endregion

        #region -- Private helpers --

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
                IsMenuVisible = true;
            }
            else 
            {
                IsSecondaryMenuVisible = true;
            }            

            return Task.CompletedTask;
        }

        private Task OnChangeProfileCommandAsync()
        {
            var parameters = new NavigationParameters()
            {
                { Constants.Navigation.User, User}
            };

            return NavigationService.NavigateAsync(nameof(ChangeProfilePage), parameters);
        }

        private Task OnBackCommandAsync()
        {
            var parameters = new NavigationParameters()
            {
                { Constants.Navigation.User, User}
            };

            return NavigationService.GoBackAsync(parameters);
        }

        private void InitTabs()
        {
            var tabs = new List<PofilePageItemViewModel>()
            {
                { new PostsViewModel("Posts", Owls)},
                { new LikesViewModel("Likes",LikedOwls) }
            };

            Tabs = new ObservableCollection<PofilePageItemViewModel>(tabs);
        }

        private async Task SetDataAsync()
        {
            IsAuthorized = User.Id == _authorizationService.AuthorizedUserId;

            var owlAOResult = _owlService.GetAuthorOwlsAsync(User.Id);
            var likedOwlsAOresult = _owlService.GetLikedOwlsAsync(User.Id);

            await Task.WhenAll(owlAOResult, likedOwlsAOresult);

            Owls = new ObservableCollection<OwlViewModel>(owlAOResult.Result.Result);
            LikedOwls = new ObservableCollection<OwlViewModel>(likedOwlsAOresult.Result.Result);
        }
        #endregion

    }
}
