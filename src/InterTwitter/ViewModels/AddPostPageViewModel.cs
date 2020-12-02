using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using InterTwitter.Services.Permission;
using InterTwitter.ViewModels.MediaItems;
using Plugin.Media.Abstractions;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class AddPostPageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMedia _mediaPlugin;
        private readonly IPermissionService _permissionService;

        private ICommand _removeItemCommand;

        public AddPostPageViewModel(INavigationService navigationService,
                                    IOwlService owlService,
                                    IMedia mediaPlugin,
                                    IPermissionService permissionService,
                                    IAuthorizationService authorizationService)
                                   : base(navigationService)
        {
            _owlService = owlService;
            _authorizationService = authorizationService;
            _mediaPlugin = mediaPlugin;
            _permissionService = permissionService;

            OwlType = OwlType.NoMedia;
            MediaItems = new ObservableCollection<MediaItemViewModel>();
            MediaButtonEnabled = true;
            GifButtonEnabled = true;
            VideoButtonEnabled = true;

            _removeItemCommand = SingleExecutionCommand.FromFunc<MediaItemViewModel>(OnRemoveItemCommandAsync);
        }

        #region -- Public Properties --

        private bool _mediaButtonEnabled;
        public bool MediaButtonEnabled
        {
            get => _mediaButtonEnabled;
            set => SetProperty(ref _mediaButtonEnabled, value);
        }

        private bool _gifButtonEnabled;
        public bool GifButtonEnabled
        {
            get => _gifButtonEnabled;
            set => SetProperty(ref _gifButtonEnabled, value);
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

        public ICommand AddPostCommand => SingleExecutionCommand.FromFunc(OnAddPostCommandAsync);

        public ICommand CancelCommand => SingleExecutionCommand.FromFunc(OnCancelCommandAsync);

        public ICommand MediaCommand => SingleExecutionCommand.FromFunc(OnMediaCommandAsync);

        public ICommand GifCommand => SingleExecutionCommand.FromFunc(OnGifCommand);

        public ICommand VideoCommand => SingleExecutionCommand.FromFunc(OnVideoCommand);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var result = await _authorizationService.GetAuthorizedUserAsync();
            var author = result.Result;

            AuthorAvatar = author.Avatar;
        }

        #endregion

        #region -- Private Helpers --

        private Task OnRemoveItemCommandAsync(MediaItemViewModel arg)
        {
            MediaItems.Remove(arg);

            RefreshCollection();

            return Task.CompletedTask;
        }

        private async Task OnCancelCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private async Task OnAddPostCommandAsync()
        {
            if (!string.IsNullOrEmpty(_owlText) && _owlText.Length > 0 && _owlText.Length < 280)
            {
                var list = new List<string>();
                foreach (MediaItemViewModel item in MediaItems)
                {
                    list.Add(item.MediaPath);
                }

                var owl = new OwlModel()
                {
                    MediaType = OwlType,
                    Date = DateTime.Now,
                    Text = OwlText,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                    Media = list
                };

                await _owlService.AddOwlAsync(owl);

                await NavigationService.GoBackAsync();
            }
        }

        private async Task OnMediaCommandAsync()
        {
            if (_mediaPlugin.IsPickPhotoSupported)
            {
                if (MediaButtonEnabled)
                {
                        MediaFile file = await _mediaPlugin.PickPhotoAsync();

                        if (file != null)
                        {
                            OwlType = _mediaItems.Count == 1 ? OwlType.OneImage : OwlType.FewImages;
                            MediaItems.Add(new MediaItemViewModel(file.Path, _removeItemCommand));
                            RefreshCollection();
                        }
                    else
                    {
                        //Currentfile == null;
                    }
                }
            }
            else
            {
                //Pick photo is not supported;
            }
        }

        private Task OnGifCommand()
        {
            return Task.FromResult(true);
        }

        private async Task OnVideoCommand()
        {
            if (_mediaPlugin.IsPickVideoSupported)
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
            else
            {
                //Pick video is not supported;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (MediaItems.Count == 0)
            {
                OwlType = OwlType.NoMedia;
            }

            if (OwlType == OwlType.OneImage || OwlType == OwlType.FewImages)
            {
                GifButtonEnabled = false;
                VideoButtonEnabled = false;

                if (MediaItems.Count == 6)
                {
                    MediaButtonEnabled = false;
                }
            }
            else if (OwlType == OwlType.Video)
            {
                MediaButtonEnabled = false;
                GifButtonEnabled = false;
                VideoButtonEnabled = false;
            }
        }

        private void RefreshCollection()
        {
            if (MediaItems is not null)
            {
                MediaItems.CollectionChanged -= OnCollectionChanged;

                MediaItems = new ObservableCollection<MediaItemViewModel>(MediaItems);

                MediaItems.CollectionChanged += OnCollectionChanged;
            }
        }

        #endregion
    }
}
