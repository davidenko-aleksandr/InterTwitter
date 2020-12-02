using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.Services.Notification;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.NotificationItems;
using InterTwitter.ViewModels.OwlItems;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class NotificationsPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INotificationService _notificationService;
        private readonly IOwlService _owlService;

        public NotificationsPageViewModel(
            IOwlService owlService,
            INavigationService navigationService,
            IUserDialogs userDialogs,
            INotificationService notificationService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _notificationService = notificationService;
            _owlService = owlService;
        }

        #region -- Public Properties --

        private string _icon = "ic_notifications_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private ObservableCollection<NotificationViewModel> _notificationList;
        public ObservableCollection<NotificationViewModel> NotificationList
        {
            get => _notificationList;
            set => SetProperty(ref _notificationList, value);
        }

        private NotificationViewModel _selectedItem;
        public NotificationViewModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        public ICommand OpenPostCommand => SingleExecutionCommand.FromFunc(OnOpenPostCommandAsync);

        #endregion

        #region -- Overrides --

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_notifications_blue";
           
            await FillNotificationListAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_notifications_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task OnOpenPostCommandAsync()
        {
            await _owlService.get
            NavigationParameters parameters = new NavigationParameters
            {
                {
                    "OwlViewModel", SelectedItem
                }
            };

            await NavigationService.NavigateAsync(nameof(PostPage), parameters, useModalNavigation: true, true);
        }

        private async Task FillNotificationListAsync()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var notificationsResult = await _notificationService.GetNotificationCollectionAsync();

                if(notificationsResult.Result is not null)
                {
                    NotificationList = notificationsResult.Result;
                }
                else
                {
                    var errorText = Resources.AppResource.RandomError;
                    _userDialogs.Toast(errorText);
                }

            }
            else
            {
                var errorText = Resources.AppResource.NoInternetText;
                _userDialogs.Toast(errorText);
            }

        }

        #endregion

    }
}
