using InterTwitter.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlFewImagesViewModel : OwlViewModel
    {
        public OwlFewImagesViewModel(
            OwlModel model,
            int authorizedUserId,
            ICommand avatarTappedCommand,
            ICommand itemTappedCommand,
            ICommand likeTappedCommad,
            ICommand saveTappedCommand)
            : base(model, authorizedUserId, avatarTappedCommand, itemTappedCommand, likeTappedCommad, saveTappedCommand)
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
