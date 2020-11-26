namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlVideoViewModel : OwlViewModel
    {
        #region -- Public properties --

        private string _video;
        public string Video
        {
            get { return _video; }
            set { SetProperty(ref _video, value); }
        }

        #endregion    
    }
}