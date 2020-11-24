using Prism.Mvvm;

namespace InterTwitter.ViewModels.HomePageItems
{
    public class OwlViewModel : BindableBase
    {

        private int _userId;
        public int UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        private int _owlId;
        public int OwlId
        {
            get { return _owlId; }
            set { SetProperty(ref _owlId, value); }
        }

        private string _authorAvatar;
        public string AuthorAvatar
        {
            get { return _authorAvatar; }
            set { SetProperty(ref _authorAvatar, value); }
        }

        private string _postLabel;
        public string PostLabel
        {
            get { return _postLabel; }
            set { SetProperty(ref _postLabel, value); }
        }

        private string _postDescription;
        public string PostDescription
        {
            get { return _postDescription; }
            set { SetProperty(ref _postDescription, value); }
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
    }
}
