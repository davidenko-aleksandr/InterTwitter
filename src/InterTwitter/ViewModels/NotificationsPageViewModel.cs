using Acr.UserDialogs;
using InterTwitter.Enums;
using InterTwitter.Models;
using InterTwitter.Services.Notification;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class NotificationsPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly INotificationService _notificationService;

        public NotificationsPageViewModel(
                                          INavigationService navigationService,
                                          IUserDialogs userDialogs,
                                          INotificationService notificationService)
                                          : base(navigationService)
        {
            _userDialogs = userDialogs;
            _notificationService = notificationService;
        }

        #region -- Public Properties --

        private string _icon = "ic_notifications_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private ObservableCollection<NotificationModel> _notificationList;
        public ObservableCollection<NotificationModel> NotificationList
        {
            get => _notificationList;
            set => SetProperty(ref _notificationList, value);
        }

        #endregion

        #region -- Overrides --

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_notifications_blue";
           
            await FillNotificationList();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_notifications_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task FillNotificationList()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var notificationsResult = await _notificationService.GetNotificationCollectionAsync();

                if(notificationsResult.Result != null)
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
