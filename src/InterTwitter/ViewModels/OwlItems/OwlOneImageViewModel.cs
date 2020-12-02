using System.Linq;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlOneImageViewModel : OwlViewModel
    {
        public OwlOneImageViewModel(
            OwlModel model, 
            UserModel author) 
            : base(model, author)
        {
            PostPhoto = model.Media.First();
        }

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
