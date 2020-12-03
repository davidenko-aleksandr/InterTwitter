using System.Linq;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.HomePageItems
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