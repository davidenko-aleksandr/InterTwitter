using System;
using System.Collections.ObjectModel;
using InterTwitter.Views;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public MenuPageViewModel(INavigationService navigationService)
                                 : base(navigationService)
        {
            var navCommand = new Command<string>(async (string pageName) => await NavigationService.NavigateAsync(pageName));

            var collection = new ObservableCollection<MenuItemGroup>
            {
                new MenuItemGroup(false)
                {
                    new MenuItemViewModel(navCommand)
                    {
                        Text = "Home",
                        PageName = nameof(HomePage)
                    },
                    new MenuItemViewModel(navCommand)
                    {
                        Text = "Search",
                        PageName = nameof(SearchPage)
                    },
                    new MenuItemViewModel(navCommand)
                    {
                        Text = "Notifications",
                        PageName = nameof(NotificationsPage)
                    },
                    new MenuItemViewModel(navCommand)
                    {
                        Text = "Direct messages",
                        PageName = nameof(MessagesPage)
                    },
                },
                new MenuItemGroup(true)
                {
                    new MenuItemViewModel(navCommand)
                    {
                        Text = "Settings",
                        PageName = nameof(MessagesPage)
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
