using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.HomePageItems;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{

    public class HomePageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
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

        private OwlPictureViewModel _owlPicture;
        public OwlPictureViewModel OwlPicture
        {
            get { return _owlPicture; }
            set { SetProperty(ref _owlPicture, value); }
        }

        private OwlAlbumViewModel _owlAlbum;
        public OwlAlbumViewModel OwlAlbum
        {
            get { return _owlAlbum; }
            set { SetProperty(ref _owlAlbum, value); }
        }

        private OwlSomePicturesViewModelcs _owlSomePictures;

        #endregion

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetOwlPicture();
            await GetOwlTwoPictures();
            await GetOwlSomePictures();

            Owls = new ObservableCollection<OwlViewModel>() { _owlPicture, _owlAlbum, _owlSomePictures };
        }

        #region -- Private helpers --

        private async Task<OwlPictureViewModel> GetOwlPicture()
        {
            _owlPicture = new OwlPictureViewModel();
            var owlService = await _owlService.GetOwlPictureAsync();

            return _owlPicture = owlService.Result;
        }

        private async Task<OwlAlbumViewModel> GetOwlTwoPictures()
        {
            _owlAlbum = new OwlAlbumViewModel();
            var owlService = await _owlService.GetOwlTwoPicturesAsync();

            return _owlAlbum = owlService.Result;
        }

        private async Task<OwlSomePicturesViewModelcs> GetOwlSomePictures()
        {
            _owlSomePictures = new OwlSomePicturesViewModelcs();
            var owlService = await _owlService.GetOwlSomePicturesAsync();

            return _owlSomePictures = owlService.Result;
        }
        #endregion
    }
}
