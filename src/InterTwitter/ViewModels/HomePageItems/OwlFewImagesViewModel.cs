namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlFewImagesViewModel : OwlViewModel
    {
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
