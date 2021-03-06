﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Keyboard;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.MediaItems;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class AddPostPageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMedia _mediaPlugin;
        private readonly IKeyboardService _keyboardService;

        private ICommand _removeItemCommand;

        public AddPostPageViewModel(
            INavigationService navigationService,
            IOwlService owlService,
            IMedia mediaPlugin,
            IKeyboardService keyboardService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            _owlService = owlService;
            _authorizationService = authorizationService;
            _mediaPlugin = mediaPlugin;
            _keyboardService = keyboardService;

            OwlType = OwlType.NoMedia;
            MediaItems = new ObservableCollection<MediaItemViewModel>();
            MediaButtonEnabled = _mediaPlugin.IsPickPhotoSupported;
            VideoButtonEnabled = _mediaPlugin.IsPickVideoSupported;
            OwlText = string.Empty;

            _keyboardService.KeyboardShown += OnKeyboardOpened;
            _keyboardService.KeyboardHidden += OnKeyboardClosed;

            _removeItemCommand = SingleExecutionCommand.FromFunc<MediaItemViewModel>(OnRemoveItemCommandAsync);
        }

        #region -- Public Properties --

        private bool _mediaButtonEnabled;
        public bool MediaButtonEnabled
        {
            get => _mediaButtonEnabled;
            set => SetProperty(ref _mediaButtonEnabled, value);
        }

        private bool _videoButtonEnabled;
        public bool VideoButtonEnabled
        {
            get => _videoButtonEnabled;
            set => SetProperty(ref _videoButtonEnabled, value);
        }

        private OwlType _owlType;
        public OwlType OwlType
        {
            get => _owlType;
            set => SetProperty(ref _owlType, value);
        }

        private ObservableCollection<MediaItemViewModel> _mediaItems;
        public ObservableCollection<MediaItemViewModel> MediaItems
        {
            get => _mediaItems;
            set => SetProperty(ref _mediaItems, value);
        }

        private string _owlText;
        public string OwlText
        {
            get => _owlText;
            set => SetProperty(ref _owlText, value);
        }

        private string _authorAvatar;
        public string AuthorAvatar
        {
            get => _authorAvatar;
            set => SetProperty(ref _authorAvatar, value);
        }

        private Thickness _toolbarMargin;
        public Thickness ToolbarMargin
        {
            get => _toolbarMargin;
            set => SetProperty(ref _toolbarMargin, value);
        }

        private int _counter;
        public int Counter
        {
            get => _counter;
            set => SetProperty(ref _counter, value);
        }

        private bool _canAddPost;
        public bool CanAddPost
        {
            get => _canAddPost;
            set => SetProperty(ref _canAddPost, value);
        }

        public ICommand AddPostCommand => SingleExecutionCommand.FromFunc(OnAddPostCommandAsync);

        public ICommand CancelCommand => SingleExecutionCommand.FromFunc(OnCancelCommandAsync);

        public ICommand MediaCommand => SingleExecutionCommand.FromFunc(OnMediaCommandAsync);

        public ICommand VideoCommand => SingleExecutionCommand.FromFunc(OnVideoCommandAsync);

        public ICommand RemoveVideoCommand => SingleExecutionCommand.FromFunc(OnRemoveVideoCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var result = await _authorizationService.GetAuthorizedUserAsync();
            var author = result.Result;

            AuthorAvatar = author.Avatar;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (OwlText != null && MediaItems != null)
            {
                if (args.PropertyName == nameof(OwlText) ||
                    args.PropertyName == nameof(MediaItems))
                {
                    Counter = Constants.MaxPostLength - OwlText.Length;

                    CanAddPost = (!string.IsNullOrEmpty(OwlText) || MediaItems.Count != 0) && OwlText.Length <= Constants.MaxPostLength;
                }
            }
        }

        #endregion

        #region -- Private Helpers --

        private Task OnRemoveItemCommandAsync(MediaItemViewModel arg)
        {
            MediaItems.Remove(arg);
            RefreshCollection();

            return Task.CompletedTask;
        }

        private Task OnRemoveVideoCommandAsync()
        {
            MediaItems.Clear();
            RefreshCollection();

            return Task.CompletedTask;
        }

        private async Task OnCancelCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private async Task OnAddPostCommandAsync()
        {
            if (CanAddPost)
            {
                var list = new List<string>(MediaItems.Select(x => x.MediaPath));

                var owl = new OwlModel()
                {
                    MediaType = OwlType,
                    Date = DateTime.Now,
                    Text = OwlText,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                    Media = list,
                };

                await _owlService.AddOwlAsync(owl);

                await NavigationService.GoBackAsync();
            }
        }

        private async Task OnMediaCommandAsync()
        {
            if (MediaButtonEnabled)
            {
                MediaFile file = await _mediaPlugin.PickPhotoAsync();

                if (file != null)
                {
                    if (OwlType == OwlType.NoMedia || !CheckMediaIsGif(file.Path))
                    {
                        MediaItems.Add(new MediaItemViewModel(file.Path, _removeItemCommand));

                        MediaButtonEnabled = !CheckMediaIsGif(file.Path);
                        OwlType = OwlType == OwlType.NoMedia ? OwlType.OneImage : OwlType.FewImages;

                        RefreshCollection();
                    }
                    else
                    {
                        //you cannot add gif after photo
                    }
                }
                else
                {
                    //Currentfile == null;
                }
            }
        }

        private async Task OnVideoCommandAsync()
        {
            if (VideoButtonEnabled)
            {
                MediaFile file = await _mediaPlugin.PickVideoAsync();

                if (file != null)
                {
                    OwlType = OwlType.Video;
                    _mediaItems.Add(new MediaItemViewModel(file.Path, _removeItemCommand));
                    RefreshCollection();
                }
                else
                {
                    //Currentfile == null;
                }
            }
        }

        private void RefreshCollection()
        {
            RaisePropertyChanged(nameof(MediaItems));

            if (MediaItems.Count < 6 && OwlType == OwlType.FewImages)
            {
                MediaButtonEnabled = true;
            }
            else if (MediaItems.Count == 0)
            {
                OwlType = OwlType.NoMedia;
                MediaButtonEnabled = true;
                VideoButtonEnabled = true;
            }
            else if ((OwlType == OwlType.OneImage && CheckMediaIsGif(MediaItems.First().MediaPath)) || OwlType == OwlType.Video)
            {
                MediaButtonEnabled = false;
                VideoButtonEnabled = false;
            }
            else if (OwlType == OwlType.OneImage || OwlType == OwlType.FewImages)
            {
                VideoButtonEnabled = false;

                if (MediaItems.Count == 6)
                {
                    MediaButtonEnabled = false;
                }
            }
        }

        private bool CheckMediaIsGif(string mediaPath)
        {
            Regex gifRegex = new Regex("([^\\s]+.(?i)(gif)$)");

            return gifRegex.IsMatch(mediaPath);
        }

        private void OnKeyboardClosed(object sender, EventArgs e)
        {
            ToolbarMargin = new Thickness(0, 0, 0, 0);
        }

        private void OnKeyboardOpened(object sender, EventArgs e)
        {
            ToolbarMargin = new Thickness(0, 0, 0, _keyboardService.FrameHeight);
        }

        #endregion
    }
}
