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
using InterTwitter.Services.PostAction;
using InterTwitter.Enums;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IUserDialogs _userDialogs;
        private readonly IAuthorizationService _authorizationService;
        private readonly IPostActionService _postActionService;

        public HomePageViewModel(
            INavigationService navigationService,
            IOwlService owlService,
            IUserDialogs userDialogs,
            IAuthorizationService authorizationService,
            IPostActionService postActionService)
            : base(navigationService)
        {
            _owlService = owlService;
            _userDialogs = userDialogs;
            _authorizationService = authorizationService;
            _postActionService = postActionService;
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

        public ICommand LikeClickCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnLikeClickCommandAsync);

        public ICommand BookmarkCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnBookmarkCommandAsync);       

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_home_blue";

            var isConnected = Connectivity.NetworkAccess;

            if (isConnected == NetworkAccess.Internet)
            {
                await FillCollectionAsync();
                await SetUserDataAsync();
            }
            else
            {
                var errorText = Resources.AppResource.NoInternetText;
                _userDialogs.Toast(errorText);
            }

        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_home_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task FillCollectionAsync()
        {
            var owlsResult = await _owlService.GetAllOwlsAsync();
            if (owlsResult.IsSuccess)
            {
                Owls = new ObservableCollection<OwlViewModel>(owlsResult.Result);
            }
            else
            {
                var errorText = Resources.AppResource.RandomError;
                _userDialogs.Toast(errorText);
            }

        }

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

        private async Task OnLikeClickCommandAsync(OwlViewModel owl)
        {            
            if (owl != null)
            {
                owl.IsLiked = !owl.IsLiked;

                owl.LikesCount = owl.IsLiked ? owl.LikesCount + 1 : owl.LikesCount - 1;   

                await _postActionService.SaveActionAsync(owl, OwlAction.Liked);
            }
            else
            {
                //something went wrong
            }
        }

        private async Task OnBookmarkCommandAsync(OwlViewModel owl)
        {
            if (owl != null)
            {
                owl.IsBookmarked = !owl.IsBookmarked;

                await _postActionService.SaveActionAsync(owl, OwlAction.Saved);

                await _postActionService.SaveActionAsync(owl, OwlAction.Saved);
            }
        }

        private async Task SetUserDataAsync()
        {
            var result = await _authorizationService.GetAuthorizedUserAsync();

            if (result.IsSuccess)
            {
                AuthorizedUser = result.Result;
            }
            else
            {
                //result is failed
            }
        }

        #endregion
    }
}