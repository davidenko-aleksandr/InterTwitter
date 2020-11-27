﻿namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlGifViewModel : OwlViewModel
    {
        #region -- Public properties --

        private string _gif;
        public string Gif
        {
            get => _gif;
            set => SetProperty(ref _gif, value);
        }

        #endregion      
    }
}