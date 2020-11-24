

namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlPictureViewModel : OwlViewModel
    {
        #region -- Public properties --

        private string _postPhoto;
        public string PostPhoto
        {
            get { return _postPhoto; }
            set
            {
                if (_postPhoto != value)
                {
                    _postPhoto = value;
                }
            }
        }

        #endregion        
    }
}
