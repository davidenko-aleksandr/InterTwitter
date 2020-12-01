using InterTwitter.Models;
using Prism.Mvvm;
using System.Collections.Generic;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlViewModel : BindableBase
    {
        public OwlViewModel(OwlModel model, UserModel author)
        {
            Id = model.Id;
            AuthorId = author.Id;
            AuthorAvatar = author.Avatar;
            AuthorNickName = author.Name;
            Text = model.Text;
            PostDate = model.Date.ToString("dd.MM.yyyy");
            PostTime = model.Date.ToString("HH:mm");
            LikesCount = model.SavesList.Count;
            LikesList = model.LikesList;
            SavesList = model.SavesList;
        }

        #region -- Public properties --

        private int _Id;
        public int Id
        {
            get => _Id;
            set => SetProperty(ref _Id, value);
        }

        private int _authorId;
        public int AuthorId
        {
            get => _authorId;
            set => SetProperty(ref _authorId, value);
        }

        private string _authorAvatar;
        public string AuthorAvatar
        {
            get => _authorAvatar;
            set => SetProperty(ref _authorAvatar, value);
        }

        private string _authorNickName;
        public string AuthorNickName
        {
            get => _authorNickName;
            set => SetProperty(ref _authorNickName, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _postDate;
        public string PostDate
        {
            get => _postDate;
            set => SetProperty(ref _postDate, value);
        }

        private string _postTime;
        public string PostTime
        {
            get => _postTime;
            set => SetProperty(ref _postTime, value);
        }

        private List<int> _likestList;
        public List<int> LikesList
        {
            get => _likestList;
            set => SetProperty(ref _likestList, value);
        }

        private List<int> _savesList;
        public List<int> SavesList
        {
            get => _savesList;
            set => SetProperty(ref _savesList, value);
        }

        private int _likesCount;
        public int LikesCount
        {
            get => _likesCount;
            set => SetProperty(ref _likesCount, value);
        }

        #endregion
    }
}
