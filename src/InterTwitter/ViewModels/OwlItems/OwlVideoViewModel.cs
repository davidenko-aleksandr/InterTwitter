using System.Linq;
using System.Windows.Input;
using InterTwitter.Models;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlVideoViewModel : OwlViewModel
    {
        public OwlVideoViewModel(
            OwlModel model,
            int authorizedUserId,
            ICommand itemTappedCommand,
            ICommand likeTappedCommad,
            ICommand saveTappedCommand)
            : base(model, authorizedUserId, itemTappedCommand, likeTappedCommad, saveTappedCommand)
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