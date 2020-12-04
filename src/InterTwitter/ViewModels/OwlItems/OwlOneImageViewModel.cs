using System.Linq;
using System.Windows.Input;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlOneImageViewModel : OwlViewModel
    {
        public OwlOneImageViewModel(
            OwlModel model,
            int authorizedUserId,
            ICommand itemTappedCommand,
            ICommand likeTappedCommad,
            ICommand saveTappedCommand)
            : base(model, authorizedUserId, itemTappedCommand, likeTappedCommad, saveTappedCommand)
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
