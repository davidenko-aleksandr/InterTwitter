using Acr.UserDialogs;
using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Services.PostAction;
using InterTwitter.ViewModels.OwlItems;
using Prism.Navigation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels
{
    public class OpenPhotoPageViewModel : BaseViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IPostActionService _postActionService;

        private OwlViewModel _owlViewModel;

        public OpenPhotoPageViewModel(
            IUserDialogs userDialogs,
            INavigationService navigationService,
            IPostActionService postActionService)
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _postActionService = postActionService;
        }

        #region -- Public properties --

        private List<string> _images;
        public List<string> Imeges
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        private int _currentImage;
        public int CurrentImage
        {
            get => _currentImage;
            set => SetProperty(ref _currentImage, value);
        }

        private int _imageNumber;
        public int ImageNumber
        {
            get => _imageNumber;
            set => SetProperty(ref _imageNumber, value);
        }

        private int _imageCount;
        public int ImageCount
        {
            get => _imageCount;
            set => SetProperty(ref _imageCount, value);
        }

        private bool _isLiked;
        public bool IsLiked
        {
            get => _isLiked;
            set => SetProperty(ref _isLiked, value);
        }

        private int _likesCount;
        public int LikesCount
        {
            get => _likesCount;
            set => SetProperty(ref _likesCount, value);
        }

        private bool _boommarked;
        public bool IsBookmarked
        {
            get => _boommarked;
            set => SetProperty(ref _boommarked, value);
        }

        private bool _IsMenuVisible;
        public bool IsMenuVisible
        {
            get => _IsMenuVisible;
            set => SetProperty(ref _IsMenuVisible, value);
        }

        public ICommand MenuClickCommand => SingleExecutionCommand.FromFunc(OnMenuClickCommand);

        public ICommand GoBackCommand => SingleExecutionCommand.FromFunc(OnGoBackCommandAsync);

        public ICommand LikeClickCommand => SingleExecutionCommand.FromFunc(OnLikeClickCommandAsync);

        public ICommand BookmarkCommand => SingleExecutionCommand.FromFunc(OnBookmarkCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(CurrentImage))
            {
                ImageNumber = CurrentImage + 1;
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var isConnected = Connectivity.NetworkAccess;

            if (isConnected == NetworkAccess.Internet)
            {
                if (parameters.TryGetValue("OwlViewModel", out _owlViewModel) && _owlViewModel != null)
                {
                    Imeges = _owlViewModel.Media;

                    ImageCount = _owlViewModel.Media.Count;

                    LikesCount = _owlViewModel.LikesCount;

                    IsBookmarked = _owlViewModel.IsBookmarked;

                    IsLiked = _owlViewModel.IsLiked;
                }
            }
            else
            {
                _userDialogs.Toast("No internet connection");
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnLikeClickCommandAsync()
        {
            if (_owlViewModel != null)
            {
                _owlViewModel.IsLiked = !_owlViewModel.IsLiked;

                IsLiked = _owlViewModel.IsLiked;

                _owlViewModel.LikesCount = _owlViewModel.IsLiked ? _owlViewModel.LikesCount + 1 : _owlViewModel.LikesCount - 1;

                LikesCount = _owlViewModel.LikesCount;

                await _postActionService.SaveActionAsync(_owlViewModel.ToModel(), OwlAction.Liked);
            }
            else
            {
                //something went wrong
            }
        }

        private async Task OnBookmarkCommandAsync()
        {
            if (_owlViewModel != null)
            {
                _owlViewModel.IsBookmarked = !_owlViewModel.IsBookmarked;

                IsBookmarked = _owlViewModel.IsBookmarked;

                await _postActionService.SaveActionAsync(_owlViewModel.ToModel(), OwlAction.Saved);
            }
            else
            {
                //something went wrong
            }
        }

        private async Task OnGoBackCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private Task OnMenuClickCommand()
        {
            IsMenuVisible = !IsMenuVisible;

            return Task.CompletedTask;
        }

        #endregion
    }
}
