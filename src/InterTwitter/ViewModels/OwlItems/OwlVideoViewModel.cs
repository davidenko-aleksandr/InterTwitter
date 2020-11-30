using System.Linq;
using InterTwitter.Models;
using InterTwitter.ViewModels.HomePageItems;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlVideoViewModel : OwlViewModel
    {
        public OwlVideoViewModel(OwlModel model, UserModel author) : base(model, author)
        {
            Video = model.Media.First();
        }

        #region -- Public properties --

        private string _video;
        public string Video
        {
            get => _video;
            set => SetProperty(ref _video, value);
        }

        #endregion    
    }
}