using System;
using System.Windows.Input;
using InterTwitter.Models;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Threading.Tasks;
using InterTwitter.Views;
using InterTwitter.Services.Authorization;
using System.Collections.ObjectModel;
using InterTwitter.Services.Owl;
using Xamarin.Forms;
using System.Collections.Generic;
using InterTwitter.ViewModels.ProfilePageItems;
using InterTwitter.ViewModels.HomePageItems;
using InterTwitter.Services.UserService;

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
                User = new UserViewModel( userAOResult.Result);
            }
            else
            {
                await SetAuthorizedUserAsync();
            }

            InitTabs();
        }        

        #endregion

        #region -- Private helpers --

        private Task OnChangeProfileCommand()
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

        #endregion

    }
}
