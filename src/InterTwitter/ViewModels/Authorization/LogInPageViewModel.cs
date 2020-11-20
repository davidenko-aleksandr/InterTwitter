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
        public ICommand ShowAlertCommand => _showAlert ?? (_showAlert = new Command(ShowAlert));

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