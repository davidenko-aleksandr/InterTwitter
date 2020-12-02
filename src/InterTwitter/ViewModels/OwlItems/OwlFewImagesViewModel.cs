using InterTwitter.Models;
using System.Collections.Generic;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlFewImagesViewModel : OwlViewModel
    {
        public OwlFewImagesViewModel(
            OwlModel model, 
            UserModel author, 
            int authorizedUserId) 
            : base(model, author, authorizedUserId)
        {
            Media = model.Media;
        }

    #region -- Public properties --

        private List<string> _media;
        public List<string> Media
        {
            get => _media;
            set => SetProperty(ref _media, value);
        }

        #endregion
    }
}
