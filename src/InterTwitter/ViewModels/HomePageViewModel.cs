using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterTwitter.Enums;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.HomePageItems;
using System;
using System.Windows.Input;
using InterTwitter.Helpers;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;

        private OwlOneImageViewModel _owlOneImage;
        private OwlAlbumViewModel _owlAlbum;
        private OwlFewImagesViewModel _owlFewImages;
        private OwlNoMediaViewModel _owlNoMedia;

        public HomePageViewModel(
                                INavigationService navigationService,
                                IOwlService owlService)
                                : base(navigationService)
        {
            _owlService = owlService;
        }

        #region -- Public properties --

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get { return _owls; }
            set { SetProperty(ref _owls, value); }
        }

        #endregion       

        #region -- Private helpers --

        private async Task<OwlOneImageViewModel> GetOwlImage()
        {
            _owlOneImage = new OwlOneImageViewModel();
            var owlService = await _owlService.GetOwlDataAsync<OwlOneImageViewModel>(OwlType.OneImage);

            return _owlOneImage = owlService.Result;
        }

        private async Task<OwlAlbumViewModel> GetOwlAlbum()
        {
            _owlAlbum = new OwlAlbumViewModel();
            var owlService = await _owlService.GetOwlDataAsync<OwlAlbumViewModel>(OwlType.Album);

            return _owlAlbum = owlService.Result;
        }

        private async Task<OwlFewImagesViewModel> GetOwlFewImages()
        {
            _owlFewImages = new OwlFewImagesViewModel();
            var owlService = await _owlService.GetOwlDataAsync<OwlFewImagesViewModel>(OwlType.FewImages);

            return _owlFewImages = owlService.Result;
        }

        private async Task<OwlNoMediaViewModel> GetOwlNoMedia()
        {
            _owlNoMedia = new OwlNoMediaViewModel();
            var owlService = await _owlService.GetOwlDataAsync<OwlNoMediaViewModel>(OwlType.NoMedia);

            return _owlNoMedia = owlService.Result;
        }

        #region -- Public Properties --

        private string _icon = "ic_home_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public ICommand OpenMenuCommand => SingleExecutionCommand.FromFunc(OnOpenMenuCommandAsync);

        private async Task OnOpenMenuCommandAsync()
        {
            MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
        }

        #endregion

        #region -- Overrides --
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_home_gray";
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_home_blue";

            await GetOwlImage();
            await GetOwlAlbum();
            await GetOwlFewImages();
            await GetOwlNoMedia();

            Owls = new ObservableCollection<OwlViewModel>() { _owlFewImages, _owlNoMedia, _owlOneImage, _owlAlbum, _owlNoMedia };
        }

        #endregion
    }
}
