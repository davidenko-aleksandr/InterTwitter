using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserDialogs _userDialogs;

        public MenuPageViewModel(INavigationService navigationService,
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

        private UserModel _authorizedUser;
        public UserModel AuthorizedUser
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
                    else if (item.PageType == typeof(MessagesPage))
                    {
                        item.Icon = item.IsSelected ? "ic_messages_blue" : "ic_messages_gray";
                    }
                }
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await SetUserDataAsync();
        }

        #endregion

        #region -- Private helpers --

        private async Task OnGoToProfilePageCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(ProfilePage), new NavigationParameters(), useModalNavigation:true, true);
        }

        private async Task OnLogoutCommandAsync()
        {
            var result = await _authorizationService.LogOutAsync();
            var isLoggedOut = result.Result;

            if (isLoggedOut)
            {
                await NavigationService.NavigateAsync($"/{nameof(LogInPage)}");
            }
            else
            {
                var errorText = Resources.AppResource.LogOutError;
                _userDialogs.Toast(errorText);
            }
      
        }

        private async Task OnSelectTabCommandAsync(MenuItemViewModel item)
        {
            NavigationService.FixedSelectTab(item.PageType);
            IsPresented = false;
        }

        private async Task OnNavigateCommandAsync(MenuItemViewModel arg) //TODO delete
        {
            await NavigationService.NavigateAsync(nameof(AddPostPage), new NavigationParameters(), useModalNavigation: true, true);
        }

        private void InitMenuItems()
        {
            ICommand selectTabCommand = SingleExecutionCommand.FromFunc<MenuItemViewModel>(OnSelectTabCommandAsync);

            ICommand navigateCommand = SingleExecutionCommand.FromFunc<MenuItemViewModel>(OnNavigateCommandAsync);//TODO delete

            var collection = new ObservableCollection<MenuItemGroup>
            {
                new MenuItemGroup(false)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Home",
                        PageType = typeof(HomePage),
                        Icon = "ic_home_gray",
                        NavigationCommand = selectTabCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Search",
                        PageType = typeof(SearchPage),
                        Icon = "ic_search_gray",
                        NavigationCommand = selectTabCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Notifications",
                        PageType = typeof(NotificationsPage),
                        Icon = "ic_notifications_gray",
                        NavigationCommand = selectTabCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Direct messages",
                        PageType = typeof(MessagesPage),
                        Icon = "ic_messages_gray",
                        NavigationCommand = selectTabCommand
                    },
                },
                new MenuItemGroup(true)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Settings",
                        PageType = typeof(AddPostPage),
                        Icon = "ic_setting",
                        NavigationCommand = navigateCommand //TODO delete
                    },
                }
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
            AuthorizedUser = result.Result;
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
