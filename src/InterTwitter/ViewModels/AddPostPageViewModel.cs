using System;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class AddPostPageViewModel : BaseViewModel
    {
        public AddPostPageViewModel(INavigationService navigationService)
                                   : base(navigationService)
        {
        }

        #region -- Public Properties --

        private string _owlText;
        public string OwlText
        {
            get => _owlText;
            set => SetProperty(ref _owlText, value);
        }

        public ICommand PostCommand => SingleExecutionCommand.FromFunc(OnPostCommandAsync);

        public ICommand CancelCommand => SingleExecutionCommand.FromFunc(OnCancelCommandAsync);

        public ICommand MediaCommand => SingleExecutionCommand.FromFunc(OnMediaCommandAsync);

        public ICommand GifCommand => SingleExecutionCommand.FromFunc(OnGifCommand);

        public ICommand VideoCommand => SingleExecutionCommand.FromFunc(OnVideoCommand);

        #endregion

        #region -- Private Helpers --

        private async Task OnCancelCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private async Task OnPostCommandAsync()
        {
            if (!string.IsNullOrEmpty(_owlText) && _owlText.Length > 0 && _owlText.Length < 280)
            {
                //owlService.AddOwl();
            }
        }

        private Task OnMediaCommandAsync()
        {
            throw new NotImplementedException();
        }

        private Task OnGifCommand()
        {
            throw new NotImplementedException();
        }

        private Task OnVideoCommand()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
