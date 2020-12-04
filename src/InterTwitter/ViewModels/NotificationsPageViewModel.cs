using Acr.UserDialogs;
using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Services.Notification;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.NotificationItems;
using InterTwitter.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
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

        private States _state;
        public States State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public ICommand OpenPostCommand => SingleExecutionCommand.FromFunc<NotificationViewModel>(OnOpenPostCommandAsync);

        #endregion

        #region -- Overrides --

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_notifications_blue";

            Connectivity.ConnectivityChanged += InternetConnectionChanged;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                State = States.Loading;
                await FillNotificationListAsync();
            }
            else
            {
                if (NotificationList == null)
                {
                    State = States.NoInternet;
                }
                else
                {
                    // notifications is not null
                }
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_notifications_gray";

            Connectivity.ConnectivityChanged -= InternetConnectionChanged;
        }

        #endregion

        #region -- Private helpers --

        private async void InternetConnectionChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                State = States.Loading;
                await FillNotificationListAsync();
            }
            else
            {
                State = States.NoInternet;
            }
        }

        private async Task OnOpenPostCommandAsync(NotificationViewModel notification)
        {
            NavigationParameters parameters = new NavigationParameters
                {
                    {
                        "OwlViewModel", notification.Owl
                    }
                };

            await NavigationService.NavigateAsync(nameof(PostPage), parameters, useModalNavigation: true, true);
        }

        private async Task FillNotificationListAsync()
        {
            var notificationsResult = await _notificationService.GetNotificationCollectionAsync();
            if (notificationsResult.IsSuccess)
            {
                NotificationList = new ObservableCollection<NotificationViewModel>(notificationsResult.Result.Select(x => x.ToViewModel(OpenPostCommand)));
                if (NotificationList == null || !NotificationList.Any())
                {
                    State = States.NoData;
                }
                else
                {
                    State = States.Normal;
                }
            }
            else
            {
                State = States.Error;
            }
        }

        #endregion

    }
}
