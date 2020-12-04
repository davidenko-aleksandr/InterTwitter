using InterTwitter.Enums;
using InterTwitter.Models;
using InterTwitter.ViewModels.OwlItems;
using Prism.Mvvm;
using System.Windows.Input;

namespace InterTwitter.ViewModels.NotificationItems
{
    public class NotificationViewModel : BindableBase
    {
        public NotificationViewModel()
        {
        }

        #region -- Public properties --

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private UserViewModel _author;
        public UserViewModel Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        private OwlViewModel _owl;
        public OwlViewModel Owl
        {
            get => _owl;
            set => SetProperty(ref _owl, value);
        }

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private OwlAction _action;
        public OwlAction Action
        {
            get => _action;
            set => SetProperty(ref _action, value);
        }

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand
        {
            get => _itemTappedCommand;
            set => SetProperty(ref _itemTappedCommand, value);
        }

        #endregion

    }
}
