﻿namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlOneImageViewModel : OwlViewModel
    {
        #region -- Public properties --

        private string _postPhoto;
        public string PostPhoto
        {
            get => _postPhoto;
            set => SetProperty(ref _postPhoto, value);
        }

        #endregion        
    }
}