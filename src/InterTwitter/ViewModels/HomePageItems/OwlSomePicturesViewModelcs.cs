namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlSomePicturesViewModelcs : OwlViewModel
    {
        #region -- Public properties --

        private string _postPhotoOne;
        public string PostPhotoOne
        {
            get { return _postPhotoOne; }
            set { SetProperty(ref _postPhotoOne, value); }
        }

        private string _postPhotoTwo;
        public string PostPhotoTwo
        {
            get { return _postPhotoTwo; }
            set { SetProperty(ref _postPhotoTwo, value); }
        }

        private string _postPhotoThree;
        public string PostPhotoThree
        {
            get { return _postPhotoThree; }
            set { SetProperty(ref _postPhotoThree, value); }
        }
        
        #endregion        
    }
}
