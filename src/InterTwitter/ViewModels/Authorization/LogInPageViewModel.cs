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
<<<<<<< HEAD
            SetStrings();
=======
            var a = 1;
>>>>>>> 9a07134f8266f0fb65fe07baf2228f67413cab0c
        }

        #region --Public properties--

        private string _firstEntry;
        public string FirstEntry
        {
            get => _firstEntry;
            set => SetProperty(ref _firstEntry, value);
        }

        private string _secondEntry;
        public string SecondEntry
        {
            get => _secondEntry;
            set => SetProperty(ref _secondEntry, value);
        }

        private string _loginButtonText;
        public string LoginButtonText
        {
            get => _loginButtonText;
            set => SetProperty(ref _loginButtonText, value);
        }

        private bool _loginLableIsVisible;
        public bool LoginLableIsVisible
        {
            get => _loginLableIsVisible;
            set => SetProperty(ref _loginLableIsVisible, value);
        }

        #endregion

        #region --Overrides--



        #endregion

        #region --Private helpers--

        private void SetStrings()
        {
            LoginButtonText = "Login";
            LoginLableIsVisible = true;
        }

        #endregion


    }
}