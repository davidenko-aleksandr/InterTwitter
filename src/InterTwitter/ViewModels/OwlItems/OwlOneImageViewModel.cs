using System.Linq;
using InterTwitter.Models;
using System.Windows.Input;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlOneImageViewModel : OwlViewModel
    {
        public OwlOneImageViewModel(
            OwlModel model,
            int authorizedUserId,
            ICommand avatarTappedCommand,
            ICommand itemTappedCommand,
            ICommand likeTappedCommad,
            ICommand saveTappedCommand)
            : base(model, authorizedUserId, avatarTappedCommand, itemTappedCommand, likeTappedCommad, saveTappedCommand)
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