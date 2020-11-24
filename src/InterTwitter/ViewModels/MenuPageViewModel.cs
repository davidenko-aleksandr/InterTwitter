using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Services.Authorization;
using InterTwitter.Views;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private readonly IAuthorizationService _authorizationService;

        public MenuPageViewModel(INavigationService navigationService,
            IAuthorizationService authorizationService)
                                 : base(navigationService)
        {
            _authorizationService = authorizationService;

            ICommand navigationCommand = SingleExecutionCommand.FromFunc<MenuItemViewModel>(OnNavigationCommandAsync);

            var collection = new ObservableCollection<MenuItemGroup>
            {
                new MenuItemGroup(false)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Home",
                        PageType = typeof(HomePage),
                        Icon = "ic_home_gray",
                        NavigationCommand = navigationCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Search",
                        PageType = typeof(SearchPage),
                        Icon = "ic_search_gray",
                        NavigationCommand = navigationCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Notifications",
                        PageType = typeof(NotificationsPage),
                        Icon = "ic_notifications_gray",
                        NavigationCommand = navigationCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Direct messages",
                        PageType = typeof(MessagesPage),
                        Icon = "ic_messages_gray",
                        NavigationCommand = navigationCommand
                    },
                },
                new MenuItemGroup(true)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Settings",
                        PageType = typeof(MessagesPage),
                        Icon = "ic_setting",
                        NavigationCommand = navigationCommand
                    },
                }
            };

            MenuItems = collection;
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

        private async Task OnGoToProfilePageCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(ProfilePage), new NavigationParameters(), useModalNavigation:true, true);
        }

        private Task OnLogoutCommandAsync()
        {
            return Task.FromResult(true);
        }

        private async Task OnNavigationCommandAsync(MenuItemViewModel item)
        {
            NavigationService.FixedSelectTab(item.PageType);
            IsPresented = false;
        }

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
    }

    public class MenuItemGroup : ObservableCollection<MenuItemViewModel>
    {
        public MenuItemGroup(bool hasSeparator)
        {
            HasSeparator = hasSeparator;
        }

        public bool HasSeparator { get; set; }
    }
}
