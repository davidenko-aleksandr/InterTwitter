using Acr.UserDialogs;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class NotificationsPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        public NotificationsPageViewModel(
                                          INavigationService navigationService,
                                          IUserDialogs userDialogs)
                                          : base(navigationService)
        {
            _userDialogs = userDialogs;
        }

        #region -- Public Properties --

        private string _icon = "ic_notifications_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private ObservableCollection<> _observablecollection;
        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_notifications_blue";

            FillNotificationList();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_notifications_gray";
        }

        #endregion

        #region -- Private helpers --

        private void FillNotificationList()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                
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
