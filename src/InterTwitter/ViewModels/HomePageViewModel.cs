using InterTwitter.Enums;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.HomePageItems;
using InterTwitter.Helpers;
using InterTwitter.Models;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
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
        private OwlGifViewModel _owlGif;
        private OwlVideoViewModel _owlVideo;

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
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        private string _icon = "ic_home_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private List<TestModel> _items;
        public List<TestModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ICommand OpenMenuCommand => SingleExecutionCommand.FromFunc(OnOpenMenuCommandAsync);

        #endregion       

        #region -- Overrides --
       
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_home_blue";
            ResetCollection();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_home_gray";
        }

        #endregion

        #region -- Private helpers --

        private async void ResetCollection()
        {
            await GetOwlImage();
            await GetOwlAlbum();
            await GetOwlFewImages();
            await GetOwlNoMedia();
            await GetOwlGif();
            await GetOwlVideo();

            Owls = new ObservableCollection<OwlViewModel>() { _owlGif, _owlFewImages, _owlOneImage, _owlAlbum, _owlNoMedia, _owlVideo };
        }

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

        private async Task<OwlGifViewModel> GetOwlGif()
        {
            _owlGif = new OwlGifViewModel();
            var owlService = await _owlService.GetOwlDataAsync<OwlGifViewModel>(OwlType.Gif);

            return _owlGif = owlService.Result;
        }

        private async Task<OwlVideoViewModel> GetOwlVideo()
        {
            _owlVideo = new OwlVideoViewModel();
            var owlService = await _owlService.GetOwlDataAsync<OwlVideoViewModel>(OwlType.Video);

            return _owlVideo = owlService.Result;
        }

        private async Task OnOpenMenuCommandAsync()
        {
            MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
        }

        #endregion

    }
}