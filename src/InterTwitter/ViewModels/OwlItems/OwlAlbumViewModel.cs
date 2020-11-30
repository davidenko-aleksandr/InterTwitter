using InterTwitter.Models;
using InterTwitter.ViewModels.HomePageItems;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlAlbumViewModel : OwlViewModel
    {
        public OwlAlbumViewModel(OwlModel model, UserModel author) : base(model, author)
        {
            PostPhotoOne = model.Media[0];
            PostPhotoTwo = model.Media[1];
            PostPhotoThree = model.Media[2];
            PostPhotoFour = model.Media[3];
            PostPhotoFive = model.Media[4];
            PostPhotoSix = model.Media[5];
        }

        #region -- Public properties --

        private string _postPhotoOne;
        public string PostPhotoOne
        {
            get => _postPhotoOne;
            set => SetProperty(ref _postPhotoOne, value);
        }

        private string _postPhotoTwo;
        public string PostPhotoTwo
        {
            get => _postPhotoTwo;
            set => SetProperty(ref _postPhotoTwo, value);
        }

        private string _postPhotoThree;
        public string PostPhotoThree
        {
            get => _postPhotoThree;
            set => SetProperty(ref _postPhotoThree, value);
        }
    
        private string _postPhotoFour;
        public string PostPhotoFour
        {
            get => _postPhotoFour;
            set => SetProperty(ref _postPhotoFour, value);
        }

        private string _postPhotoFive;
        public string PostPhotoFive
        {
            get => _postPhotoFive;
            set => SetProperty(ref _postPhotoFive, value);
        }

        private string _postPhotoSix;
        public string PostPhotoSix
        {
            get => _postPhotoSix;
            set => SetProperty(ref _postPhotoSix, value);
        }

        #endregion        
    }
}
