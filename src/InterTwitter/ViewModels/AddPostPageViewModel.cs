using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using InterTwitter.Services.Permission;
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
            OwlMedia = new ObservableCollection<string>();
        }

        #region -- Public Properties --

        private OwlType _owlType;
        public OwlType OwlType
        {
            get => _owlType;
            set => SetProperty(ref _owlType, value);
        }

        private ObservableCollection<string> _owlMedia;
        public ObservableCollection<string> OwlMedia
        {
            get => _owlMedia;
            set => SetProperty(ref _owlMedia, value);
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

        private async Task OnCancelCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private async Task OnAddPostCommandAsync()
        {
            if (!string.IsNullOrEmpty(_owlText) && _owlText.Length > 0 && _owlText.Length < 280)
            {
                var owl = new OwlModel()
                {
                    MediaType = OwlType,
                    Date = DateTime.Now,
                    Text = OwlText,
                    Media = new List<string>(OwlMedia)
                };

                await _owlService.AddOwlAsync(owl);

                await NavigationService.GoBackAsync();
            }
        }

        private async Task OnMediaCommandAsync()
        {
            if (_mediaPlugin.IsPickPhotoSupported)
            {
                if (_owlType == OwlType.Video || _owlType == OwlType.Gif)
                {
                    _owlMedia.Clear();
                }
                else
                {
                    //_owlType is compatible with adding images
                }

                if (_owlMedia.Count < 6)
                {
                    MediaFile file = await _mediaPlugin.PickPhotoAsync();

                    if (file != null)
                    {
                        OwlMedia.Add(file.Path);
                        OwlMedia = new ObservableCollection<string>(OwlMedia);
                        OwlType = _owlMedia.Count == 1 ? OwlType.OneImage : OwlType.FewImages;
                    }
                    else
                    {
                        //Currentfile == null;
                    }
                }
                else
                {
                    //TODO toast max 6 photos
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
                if (_owlType == OwlType.OneImage || _owlType == OwlType.FewImages || _owlType == OwlType.Gif)
                {
                    _owlMedia.Clear();
                }
                else
                {
                    //_owlType is compatible with adding video
                }

                if (_owlMedia.Count == 0)
                {
                    MediaFile file = await _mediaPlugin.PickVideoAsync();

                    if (file != null)
                    {
                        _owlMedia.Add(file.Path);
                        OwlMedia = new ObservableCollection<string>(OwlMedia);
                        OwlType = OwlType.Video;
                    }
                    else
                    {
                        //Currentfile == null;
                    }
                }
                else
                {
                    //TODO toast max 1 video
                }
            }
            else
            {
                //Pick video is not supported;
            }
        }

        #endregion
    }
}
