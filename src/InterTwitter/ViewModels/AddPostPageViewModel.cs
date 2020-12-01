using System;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class AddPostPageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IAuthorizationService _authorizationService;
        private OwlType _owlType = OwlType.NoMedia;

        public AddPostPageViewModel(INavigationService navigationService,
                                    IOwlService owlService,
                                    IAuthorizationService authorizationService)
                                   : base(navigationService)
        {
            _owlService = owlService;
            _authorizationService = authorizationService;
        }

        #region -- Public Properties --

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
                    MediaType = _owlType,
                    Date = DateTime.Now,
                    Text = _owlText
                };

                await _owlService.AddOwlAsync(owl);

                await NavigationService.GoBackAsync();
            }
        }

        private Task OnMediaCommandAsync()
        {
            return Task.FromResult(true);
        }

        private Task OnGifCommand()
        {
            return Task.FromResult(true);
        }

        private Task OnVideoCommand()
        {
            return Task.FromResult(true);
        }

        #endregion
    }
}
