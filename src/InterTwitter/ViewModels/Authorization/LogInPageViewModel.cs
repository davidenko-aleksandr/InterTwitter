using System;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels.Authorization
{
    public class LogInPageViewModel : BaseViewModel
    {
        public LogInPageViewModel(INavigationService navigationService)
                                 : base(navigationService)
        {

        }

        private ICommand _showAlert;
        public ICommand ShowAlertCommand => _showAlert ??= new Command(ShowAlert);

        #region -- ViewModelBase implementation --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var a = 1;
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            var a = 1;

        }

        #endregion

        private void ShowAlert()
        {
            TextOne += "One";
        }

        private string _noPins = string.Empty;
        public string TextOne
        {
            get { return _noPins; }
            set { SetProperty(ref _noPins, value); }
        }
    }
}