using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Services.Authorization;
using InterTwitter.Views;
using Prism.Navigation;
using TikBid.Helpers;
using Xamarin.Forms;

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

            var collection = new ObservableCollection<MenuItemGroup>
            {
                new MenuItemGroup(false)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Home",
                        PageName = nameof(HomePage),
                        Icon = "ic_home_gray"
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Search",
                        PageName = nameof(SearchPage),
                        Icon = "ic_search_gray"
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Notifications",
                        PageName = nameof(NotificationsPage),
                        Icon = "ic_notifications_gray"
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Direct messages",
                        PageName = nameof(MessagesPage),
                        Icon = "ic_messages_gray"
                    },
                },
                new MenuItemGroup(true)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Settings",
                        PageName = nameof(MessagesPage),
                        Icon = "ic_setting"
                    },
                }
            };

            MenuItems = collection;

            IsPresented = true;
        }

        private bool _IsPresented;
        public bool IsPresented
        {
            get => _IsPresented;
            set => SetProperty(ref _IsPresented, value);
        }

        private ObservableCollection<MenuItemGroup> _menuItems;
        public ObservableCollection<MenuItemGroup> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public ICommand NavigationCommand => SingleExecutionCommand.FromFunc<MenuItemViewModel>(OnNavigationCommandAsync);

        public ICommand LogoutCommand => SingleExecutionCommand.FromFunc(OnLogoutCommandAsync);

        private Task OnLogoutCommandAsync()
        {
            return Task.FromResult(true);
        }

        private async Task OnNavigationCommandAsync(MenuItemViewModel item)
        {
            await NavigationService.NavigateAsync(item.PageName);
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
