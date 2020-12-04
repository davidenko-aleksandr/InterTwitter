using InterTwitter.ViewModels.OwlItems;
using System.Collections.ObjectModel;

namespace InterTwitter.ViewModels.ProfilePageItems
{
    public class LikesViewModel : PofilePageItemViewModel
    {
        public LikesViewModel(string title, ObservableCollection<OwlViewModel> likes)
            : base(title)
        {
            Likes = likes;
        }

        #region -- Public properties --

        private ObservableCollection<OwlViewModel> _likes;
        public ObservableCollection<OwlViewModel> Likes
        {
            get => _likes;
            set => SetProperty(ref _likes, value);
        }

        #endregion

    }
}
