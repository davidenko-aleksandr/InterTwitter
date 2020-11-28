using InterTwitter.Models;
using Prism.Mvvm;

namespace InterTwitter.ViewModels.NotificationPageItems
{
    public class NotificationViewModel : BindableBase
    {
        public NotificationViewModel(OwlModel owl, UserModel user)
        {
            Owl = owl;
            NotificationUser = user;

            FormatTheText();
        }

        #region -- Public properties --

        private OwlModel _owl;
        public OwlModel Owl
        {
            get => _owl;
            set => SetProperty(ref _owl, value);
        }

        private UserModel _notificationUser;
        public UserModel NotificationUser
        {
            get => _notificationUser;
            set => SetProperty(ref _notificationUser, value);
        }

        private string _actionIcon;
        public string ActionIcon
        {
            get => _actionIcon;
            set => SetProperty(ref _actionIcon, value);
        }

        private string _actionText;
        public string ActionText
        {
            get => _actionText;
            set => SetProperty(ref _actionText, value);
        }

        private string _owlText;
        public string OwlText
        {
            get => _owlText;
            set => SetProperty(ref _owlText, value);
        }

        #endregion

        #region -- Private helpers --

        private void FormatTheText()
        {
            OwlText = Owl.Text.Substring(0, 39) + "...";
        }

        #endregion

    }
}
