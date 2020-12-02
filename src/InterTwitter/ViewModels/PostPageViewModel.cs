using Acr.UserDialogs;
using InterTwitter.Helpers;
using InterTwitter.ViewModels.OwlItems;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private string _authorNickName = "ic_home_gray";
        public string AuthorNickName
        {
            get => _authorNickName;
            set => SetProperty(ref _authorNickName, value);
        }

        private string _AuthorAvatar = "ic_home_gray";
        public string AuthorAvatar
        {
            get => _AuthorAvatar;
            set => SetProperty(ref _AuthorAvatar, value);
        }

        public ICommand GoBackCommand => SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        #endregion

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var isConnected = Connectivity.NetworkAccess;

            if (isConnected == NetworkAccess.Internet)
            {
                if (parameters.TryGetValue("OwlViewModel", out _owlViewModel) && _owlViewModel != null)
                {
                    Owls = new ObservableCollection<OwlViewModel>() { _owlViewModel };

                    AuthorNickName = _owlViewModel.AuthorNickName;
                    AuthorAvatar = _owlViewModel.AuthorAvatar;
                }
            }
            else
            {
                _userDialogs.Toast("No internet connection");
            }

        }

        private async Task OnGoBackCommandAsync()
        {
           await NavigationService.GoBackAsync();
        }
    }
}
