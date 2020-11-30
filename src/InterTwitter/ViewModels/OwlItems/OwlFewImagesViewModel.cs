using InterTwitter.Models;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlFewImagesViewModel : OwlViewModel
    {
        public OwlFewImagesViewModel(OwlModel model, UserModel author) : base(model, author)
        {
            PostPhotoOne = model.Media[0];
            PostPhotoTwo = model.Media[1];
            //PostPhotoThree = model.Media[2]; //TODO rework after new ListView
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
        
        #endregion        
    }
}
