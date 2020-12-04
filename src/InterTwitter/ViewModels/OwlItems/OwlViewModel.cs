using InterTwitter.Enums;
using InterTwitter.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace InterTwitter.ViewModels.OwlItems
{
    public class OwlViewModel : BindableBase
    {
        public OwlViewModel()
        {
        }

        public OwlViewModel(
            OwlModel model,
            int authorizedUserId,
            ICommand avatarTappedCommand,
            ICommand itemTappedCommand,
            ICommand likeTappedCommad,
            ICommand saveTappedCommand)
        {
            Id = model.Id;
            Author = model.Author;
            Text = model.Text;
            PostDate = model.Date.ToString("dd.MM.yyyy");
            PostTime = model.Date.ToString("HH:mm");
            LikesCount = model.LikesList.Count;
            IsLiked = model.LikesList.Contains(authorizedUserId);
            IsBookmarked = model.SavesList.Contains(authorizedUserId);
            LikesList = model.LikesList;
            SavesList = model.SavesList;
            Media = model.Media;
            Date = model.Date;
            MediaType = model.MediaType;
            AvatarTappedCommand = avatarTappedCommand;
            ItemTappedCommand = itemTappedCommand;
            LikeTappedCommand = likeTappedCommad;
            SaveTappedCommand = saveTappedCommand;
            AllHashtags = new List<string>(Text.Split().Where(x => Regex.IsMatch(x, Constants.RegexHashtag)));
        }

        #region -- Public properties --

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private int _Id;
        public int Id
        {
            get => _Id;
            set => SetProperty(ref _Id, value);
        }

        private UserModel _author;
        public UserModel Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
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

        private bool _isBookmarked;
        public bool IsBookmarked
        {
            get => _isBookmarked;
            set => SetProperty(ref _isBookmarked, value);
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

        private OwlType _mediaType;
        public OwlType MediaType
        {
            get => _mediaType;
            set => SetProperty(ref _mediaType, value);
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

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private ICommand _avatarTappedCommand;
        public ICommand AvatarTappedCommand
        {
            get => _avatarTappedCommand;
            set => SetProperty(ref _avatarTappedCommand, value);
        }

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand
        {
            get => _itemTappedCommand;
            set => SetProperty(ref _itemTappedCommand, value);
        }

        private ICommand _likeTappedCommand;
        public ICommand LikeTappedCommand
        {
            get => _likeTappedCommand;
            set => SetProperty(ref _likeTappedCommand, value);
        }

        private ICommand _saveTappedCommand;
        public ICommand SaveTappedCommand
        {
            get => _saveTappedCommand;
            set => SetProperty(ref _saveTappedCommand, value);
        }

    #endregion
}
}
