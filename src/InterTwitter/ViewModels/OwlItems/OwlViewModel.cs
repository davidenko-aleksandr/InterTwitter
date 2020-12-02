using InterTwitter.Enums;
using InterTwitter.Models;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlViewModel : BindableBase
    {
        public OwlViewModel() { }

        public OwlViewModel(
            OwlModel model,
            UserModel author,
            int authorizedUserId)
        {
            Id = model.Id;
            AuthorId = author.Id;
            AuthorAvatar = author.Avatar;
            AuthorNickName = author.Name;
            Text = model.Text;
            PostDate = model.Date.ToString("dd.MM.yyyy");
            PostTime = model.Date.ToString("HH:mm");
            LikesCount = model.LikesList.Count;
            IsLiked = model.LikesList.Contains(authorizedUserId);
            IsBookMarked = model.SavesList.Contains(authorizedUserId);
            LikesList = model.LikesList;
            SavesList = model.SavesList;
            Media = model.Media;
            MediaType = model.MediaType;
            AllHashtags = new List<string>();

            foreach (var word in Text.Split(' '))
            {
                var match = Regex.Match(word, Constants.RegexHashtag);

                if (match.Success)
                {
                    AllHashtags.Add(match.Value);
                }
            }
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

        private int _likesCount;
        public int LikesCount
        {
            get => _likesCount;
            set => SetProperty(ref _likesCount, value);
        }

        private bool _isLiked;
        public bool IsLiked
        {
            get => _isLiked;
            set => SetProperty(ref _isLiked, value);
        }

        private bool _isBookMarked;
        public bool IsBookMarked
        {
            get => _isBookMarked;
            set => SetProperty(ref _isBookMarked, value);
        }

        private List<int> _likesList;
        public List<int> LikesList
        {
            get => _likesList;
            set => SetProperty(ref _likesList, value);
        }

        private List<int> _savesList;
        public List<int> SavesList
        {
            get => _savesList;
            set => SetProperty(ref _savesList, value);
        }

        private List<string> _media;
        public List<string> Media
        {
            get => _media;
            set => SetProperty(ref _media, value);
        }

        private OwlType _owlType;
        public OwlType MediaType
        {
            get => _owlType;
            set => SetProperty(ref _owlType, value);
        }

        private List<string> _allHashtags;
        public List<string> AllHashtags
        {
            get => _allHashtags;
            set => SetProperty(ref _allHashtags, value);
        }

        private string _currentHashtag;
        public string CurrentHashtag
        {
            get => _currentHashtag;
            set => SetProperty(ref _currentHashtag, value);
        }

        #endregion
    }
}
