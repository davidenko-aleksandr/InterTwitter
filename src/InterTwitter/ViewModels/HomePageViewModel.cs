using System;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel(INavigationService navigationService)
                                : base(navigationService)
        {
        }

        #region -- Public Properties --

        private string _icon = "ic_home_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public ICommand OpenMenuCommand => SingleExecutionCommand.FromFunc(OnOpenMenuCommandAsync);

        private async Task OnOpenMenuCommandAsync()
        {
            MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_home_blue";
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_home_gray";
        }

        #endregion
    }
}
