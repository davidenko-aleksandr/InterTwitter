using InterTwitter.Enums;
using Prism.Mvvm;
using System.Collections.Generic;

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

        private int _authorId;
        public int AuthorId
        {
            get => _authorId;
            set => SetProperty(ref _authorId, value);
        }

        private int _owlId;
        public int OwlId
        {
            get => _owlId;
            set => SetProperty(ref _owlId, value);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        private string _userAvatar;
        public string UserAvatar
        {
            get => _userAvatar;
            set => SetProperty(ref _userAvatar, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private OwlAction _action;
        public OwlAction Action
        {
            get => _action;
            set => SetProperty(ref _action, value);
        }

        private OwlType _mediaType;
        public OwlType MediaType
        {
            get => _mediaType;
            set => SetProperty(ref _mediaType, value);
        }

        private string _owlText;
        public string OwlText
        {
            get => _owlText;
            set => SetProperty(ref _owlText, value);
        }

        private List<string> _mediaList;
        public List<string> MediaList
        {
            get => _mediaList;
            set => SetProperty(ref _mediaList, value);
        }

        #endregion

    }
}
