using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Resources;
using InterTwitter.Services.Authorization;
using InterTwitter.ViewModels.Helpers;
using InterTwitter.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class MenuPageViewModel : BaseViewModel, IAppearingAware
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;

        public MenuPageViewModel(
            INavigationService navigationService,
            IAuthorizationService authorizationService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _authorizationService = authorizationService;
            _userDialogs = userDialogs;

            MessagingCenter.Subscribe<object>(this, Constants.OpenMenuMessage, (sender) => OpenMenu());

            InitMenuItems();
        }

        #region -- Public properties --

        private UserViewModel _authorizedUser;
        public UserViewModel AuthorizedUser
        {
            get => _authorizedUser;
            set => SetProperty(ref _authorizedUser, value);
        }

        private bool _isPresented;
        public bool IsPresented
        {
            get => _isPresented;
            set => SetProperty(ref _isPresented, value);
        }

        private Type _selectedTabType;
        public Type SelectedTabType
        {
            get => _selectedTabType;
            set => SetProperty(ref _selectedTabType, value);
        }

        private ObservableCollection<MenuItemGroup> _menuItems;
        public ObservableCollection<MenuItemGroup> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public ICommand LogoutCommand => SingleExecutionCommand.FromFunc(OnLogoutCommandAsync);

        public ICommand GoToProfilePageCommand => SingleExecutionCommand.FromFunc(OnGoToProfilePageCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsPresented))
            {
                foreach (MenuItemViewModel item in MenuItems[0])
                {
                    item.IsSelected = SelectedTabType == item.PageType;

                    if (item.PageType == typeof(HomePage))
                    {
                        item.Icon = item.IsSelected ? "ic_home_blue" : "ic_home_gray";
                    }
                    else if (item.PageType == typeof(SearchPage))
                    {
                        item.Icon = item.IsSelected ? "ic_search_blue" : "ic_search_gray";
                    }
                    else if (item.PageType == typeof(NotificationsPage))
                    {
                        item.Icon = item.IsSelected ? "ic_notifications_blue" : "ic_notifications_gray";
                    }
                    else if (item.PageType == typeof(BookmarksPage))
                    {
                        item.Icon = item.IsSelected ? "ic_bookmarks_blue" : "ic_bookmarks_gray";
                    }
                    else
                    {
                        //page type != set
                    }
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnGoToProfilePageCommandAsync()
        {
            var navParameters = new NavigationParameters();

            navParameters.Add(Constants.Navigation.User, AuthorizedUser);

            await NavigationService.NavigateAsync(nameof(ProfilePage), navParameters, useModalNavigation: true, true);
        }

        private async Task OnLogoutCommandAsync()
        {
            bool isConfirmed = await _userDialogs.ConfirmAsync(AppResource.ConfirmLogout, null, AppResource.ContinueButton, AppResource.CancelButton);

            if (isConfirmed)
            {
                var result = await _authorizationService.LogOutAsync();
                var isLoggedOut = result.Result;

                if (isLoggedOut)
                {
                    await NavigationService.NavigateAsync($"/{nameof(LogInPage)}");
                }
                else
                {
                    var errorText = Resources.AppResource.RandomError;
                    _userDialogs.Toast(errorText);
                }
            }
            else
            {
                //User cancelled logout
            }
        }

        private async Task OnSelectTabCommandAsync(MenuItemViewModel item)
        {
            NavigationService.FixedSelectTab(item.PageType);
            IsPresented = false;
        }

        private async Task OnNavigateCommandAsync(MenuItemViewModel arg)
        {
            //navigate to settings
        }

        private void InitMenuItems()
        {
            ICommand selectTabCommand = SingleExecutionCommand.FromFunc<MenuItemViewModel>(OnSelectTabCommandAsync);

            ICommand navigateCommand = SingleExecutionCommand.FromFunc<MenuItemViewModel>(OnNavigateCommandAsync);

            var collection = new ObservableCollection<MenuItemGroup>
            {
                new MenuItemGroup(false)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Home",
                        PageType = typeof(HomePage),
                        Icon = "ic_home_gray",
                        NavigationCommand = selectTabCommand,
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Search",
                        PageType = typeof(SearchPage),
                        Icon = "ic_search_gray",
                        NavigationCommand = selectTabCommand,
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Notifications",
                        PageType = typeof(NotificationsPage),
                        Icon = "ic_notifications_gray",
                        NavigationCommand = selectTabCommand,
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Bookmarks",
                        PageType = typeof(BookmarksPage),
                        Icon = "ic_messages_gray",
                        NavigationCommand = selectTabCommand,
                    },
                },
                new MenuItemGroup(true)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Settings",
                        PageType = typeof(AddPostPage),
                        Icon = "ic_setting",
                        NavigationCommand = navigateCommand,
                    },
                },
            };

            MenuItems = collection;
        }

        private void OpenMenu()
        {
            IsPresented = true;
        }

        private async Task SetUserDataAsync()
        {
            var result = await _authorizationService.GetAuthorizedUserAsync();

            if (result.IsSuccess)
            {
                var userResult = result.Result;

                if (userResult != null)
                {
                    AuthorizedUser = userResult.ToViewModel();
                }
                else
                {
                    //userResult was null
                }
            }
            else
            {
                //result is failed
            }
        }

        public async void OnAppearing()
        {
            await SetUserDataAsync();
        }

        public void OnDisappearing()
        {
        }

        #endregion

    }

    public class MenuItemGroup : ObservableCollection<MenuItemViewModel>
    {
        public MenuItemGroup(bool hasSeparator)
        {
            HasSeparator = hasSeparator;
        }

        #region -- Public properties --

        public bool HasSeparator { get; set; }

        #endregion
    }
}
