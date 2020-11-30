using InterTwitter.Models;
using System.Collections.Generic;

namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlFewImagesViewModel : OwlViewModel
    {
        public OwlFewImagesViewModel(OwlModel model, UserModel author) : base(model, author)
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
