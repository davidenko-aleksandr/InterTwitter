using InterTwitter.ViewModels.OwlItems;
using System.Collections.ObjectModel;

namespace InterTwitter.ViewModels.ProfilePageItems
{
    public class PostsViewModel : ProfilePageItemViewModel
    {
        public PostsViewModel(string title, ObservableCollection<OwlViewModel> owls)
            : base(title)
        {
            Owls = owls;
        }

        #region -- Public properties --

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        #endregion
    }
}
