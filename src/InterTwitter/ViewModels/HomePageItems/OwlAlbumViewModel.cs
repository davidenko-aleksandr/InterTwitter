﻿namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlAlbumViewModel : OwlViewModel
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
