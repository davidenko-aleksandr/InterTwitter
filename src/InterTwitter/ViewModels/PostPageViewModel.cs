using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Services.PostAction;
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
        private readonly IPostActionService _postActionService;

        private OwlViewModel _owlViewModel;

        public PostPageViewModel(
            INavigationService navigationService,
            IPostActionService postActionService)
            : base(navigationService)
        {
            _postActionService = postActionService;
        }

        #region -- Public properties --

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        private string _authorNickName;
        public string AuthorNickName
        {
            get => _authorNickName;
            set => SetProperty(ref _authorNickName, value);
        }

        private string _AuthorAvatar;
        public string AuthorAvatar
        {
            get => _AuthorAvatar;
            set => SetProperty(ref _AuthorAvatar, value);
        }

        private States _state;
        public States State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public ICommand GoBackCommand => SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);
        public ICommand LikeClickCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnLikeClickCommandAsync);
        public ICommand BookmarkCommand => SingleExecutionCommand.FromFunc<OwlViewModel>(OnBookmarkCommandAsync);

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var isConnected = Connectivity.NetworkAccess;

            State = States.Loading;
            if (parameters.TryGetValue("OwlViewModel", out _owlViewModel) && _owlViewModel != null)
            {
                FillPost();
            }
            else
            {
                if (Owls == null)
                {
                    State = States.NoData;
                }
                else
                {
                    //Owls is not null
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private void FillPost()
        {
            _owlViewModel.LikeTappedCommand = LikeClickCommand;
            _owlViewModel.SaveTappedCommand = BookmarkCommand;
            Owls = new ObservableCollection<OwlViewModel>() { _owlViewModel };
            AuthorNickName = _owlViewModel.Author.Name;
            AuthorAvatar = _owlViewModel.Author.Avatar;

            State = States.Normal;
        }

        private Task OnGoBackCommandAsync()
        {
            return NavigationService.GoBackAsync();
        }

        private async Task OnLikeClickCommandAsync(OwlViewModel owl)
        {
            if (owl != null)
            {
                owl.IsLiked = !owl.IsLiked;
                owl.LikesCount = owl.IsLiked ? owl.LikesCount + 1 : owl.LikesCount - 1;

                await _postActionService.SaveActionAsync(owl.ToModel(), OwlAction.Liked);
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

                await _postActionService.SaveActionAsync(owl.ToModel(), OwlAction.Saved);
            }
            else
            {
                //something went wrong
            }
        }

        #endregion
    }
}
