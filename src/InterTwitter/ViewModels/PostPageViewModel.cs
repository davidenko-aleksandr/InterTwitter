using Acr.UserDialogs;
using InterTwitter.Models;
using InterTwitter.ViewModels.HomePageItems;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class PostPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;

        private OwlViewModel _owlViewModel;

        public PostPageViewModel(
                                INavigationService navigationService,
                                IUserDialogs userDialogs)
                                : base(navigationService)
        {
            _userDialogs = userDialogs;
        }

        #region -- Public properties --

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        private string _icon = "ic_home_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        #endregion

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var isConnected = Connectivity.NetworkAccess;

            if (isConnected == NetworkAccess.Internet)
            {
                Icon = "ic_home_blue";

                if (parameters.TryGetValue("OwlViewModel", out _owlViewModel) && _owlViewModel != null)
                {
                    Owls = new ObservableCollection<OwlViewModel>() { _owlViewModel };
                }
            }
            else
            {
                _userDialogs.Toast("No internet connection");
            }

        }
    }
}
