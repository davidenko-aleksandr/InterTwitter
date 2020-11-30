using System.Linq;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlGifViewModel : OwlViewModel
    {
        public OwlGifViewModel(OwlModel model, UserModel author) : base(model, author)
        {
            Gif = model.Media.First();
        }

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
