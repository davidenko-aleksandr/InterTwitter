using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            ICommand navigationCommand = SingleExecutionCommand.FromFunc<MenuItemViewModel>(OnNavigationCommandAsync);

            var collection = new ObservableCollection<MenuItemGroup>
            {
                new MenuItemGroup(false)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Home",
                        PageName = nameof(HomePage),
                        Icon = "ic_home_gray",
                        NavigationCommand = navigationCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Search",
                        PageName = nameof(SearchPage),
                        Icon = "ic_search_gray",
                        NavigationCommand = navigationCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Notifications",
                        PageName = nameof(NotificationsPage),
                        Icon = "ic_notifications_gray",
                        NavigationCommand = navigationCommand
                    },
                    new MenuItemViewModel()
                    {
                        Text = "Direct messages",
                        PageName = nameof(MessagesPage),
                        Icon = "ic_messages_gray",
                        NavigationCommand = navigationCommand
                    },
                },
                new MenuItemGroup(true)
                {
                    new MenuItemViewModel()
                    {
                        Text = "Settings",
                        PageName = nameof(MessagesPage),
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

        private string _selectedTabName;
        public string SelectedTabName
        {
            get => _selectedTabName;
            set => SetProperty(ref _selectedTabName, value);
        }

        private ObservableCollection<MenuItemGroup> _menuItems;
        public ObservableCollection<MenuItemGroup> MenuItems
        {
            get => _menuItems;
            set => SetProperty(ref _menuItems, value);
        }

        public ICommand LogoutCommand => SingleExecutionCommand.FromFunc(OnLogoutCommandAsync);

        private Task OnLogoutCommandAsync()
        {
            return Task.FromResult(true);
        }

        private async Task OnNavigationCommandAsync(MenuItemViewModel item)
        {
            await NavigationService.NavigateAsync(item.PageName);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(IsPresented))
            {
                foreach (MenuItemViewModel item in MenuItems[0])
                {
                    bool isSelected = SelectedTabName == item.PageName;
                    item.IsSelected = isSelected;
                    switch (item.PageName)
                    {
                        case nameof(HomePage):
                            item.Icon = isSelected ? "ic_home_blue" : "ic_home_gray";
                            break;
                        case nameof(SearchPage):
                            item.Icon = isSelected ? "ic_search_blue" : "ic_search_gray";
                            break;
                        case nameof(NotificationsPage):
                            item.Icon = isSelected ? "ic_notifications_blue" : "ic_notifications_gray";
                            break;
                        case nameof(MessagesPage):
                            item.Icon = isSelected ? "ic_messages_blue" : "ic_messages_gray";
                            break;
                        default:
                            break;
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
