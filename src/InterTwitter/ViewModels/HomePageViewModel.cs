using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterTwitter.Services.Owl;
using InterTwitter.Helpers;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using InterTwitter.ViewModels.OwlItems;
using InterTwitter.Views;
using Xamarin.Essentials;
using Acr.UserDialogs;
using InterTwitter.Services.Authorization;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IUserDialogs _userDialogs;
        private readonly IAuthorizationService _authorizationService;

        public HomePageViewModel(
                                INavigationService navigationService,
                                IOwlService owlService,
                                IUserDialogs userDialogs,
                                IAuthorizationService authorizationService)
                                : base(navigationService)
        {
            _owlService = owlService;
            _userDialogs = userDialogs;
            _authorizationService = authorizationService;
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

        private UserViewModel _authorizedUser;
        public UserViewModel AuthorizedUser
        {
            get => _authorizedUser;
            set => SetProperty(ref _authorizedUser, value);
        }

        private OwlViewModel selectedItem;
        public OwlViewModel SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);            
        }

        public ICommand OpenMenuCommand => SingleExecutionCommand.FromFunc(OnOpenMenuCommandAsync);

        public ICommand OpenPostCommand => SingleExecutionCommand.FromFunc(OnOpenPostCommandAsync);

        public ICommand AddPostCommand => SingleExecutionCommand.FromFunc(OnAddPostCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var isConnected = Connectivity.NetworkAccess;

            if (isConnected == NetworkAccess.Internet)
            {
                Icon = "ic_home_blue";

                var owls = await _owlService.GetAllOwlsAsync();

                Owls = new ObservableCollection<OwlViewModel>(owls.Result);
            }
            else
            {
                _userDialogs.Toast("No internet connection");
            }

            await SetUserDataAsync();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_home_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task OnOpenMenuCommandAsync()
        {
            MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
        }

        private async Task OnOpenPostCommandAsync()
        {             
            NavigationParameters parameters = new NavigationParameters 
            { 
                {
                    "OwlViewModel", SelectedItem 
                }
            };

            await NavigationService.NavigateAsync(nameof(PostPage), parameters, useModalNavigation: true, true);
        }
        
        private async Task OnAddPostCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(AddPostPage), new NavigationParameters(), useModalNavigation: true, true);
        }

        private async Task SetUserDataAsync()
        {
            var result = await _authorizationService.GetAuthorizedUserAsync();

            if (result.IsSuccess)
            {
                var userResult = result.Result;

                if (userResult is not null)
                {
                    AuthorizedUser = userResult;
                }
                else
                {
                    //userResult was null
                }

            }
            else
            {
                //result is failed
            }
        }

        #endregion
    }
}