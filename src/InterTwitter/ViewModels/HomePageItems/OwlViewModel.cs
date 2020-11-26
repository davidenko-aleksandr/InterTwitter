using Prism.Mvvm;

namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlViewModel : BindableBase
    {
        #region -- Public properties --

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }

        private int _authorId;
        public int AuthorId
        {
            get { return _authorId; }
            set { SetProperty(ref _authorId, value); }
        }

        private string _authorAvatar;
        public string AuthorAvatar
        {
            get { return _authorAvatar; }
            set { SetProperty(ref _authorAvatar, value); }
        }

        private string _authorNickName;
        public string AuthorNickName
        {
            get { return _authorNickName; }
            set { SetProperty(ref _authorNickName, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        private string _postDate;
        public string PostDate
        {
            get { return _postDate; }
            set { SetProperty(ref _postDate, value); }
        }

        private string _postTime;
        public string PostTime
        {
            get { return _postTime; }
            set { SetProperty(ref _postTime, value); }
        }

        #endregion
    }
}