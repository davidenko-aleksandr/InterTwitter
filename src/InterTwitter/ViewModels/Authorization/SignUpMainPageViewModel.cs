using System;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Helpers;
using InterTwitter.Services.Keyboard;
using Prism.Navigation;

namespace InterTwitter.ViewModels.Authorization
{
    public class SignUpMainPageViewModel : BaseViewModel
    {
        public SignUpMainPageViewModel(
            INavigationService navigationService,
            IKeyboardService keyboardService)
            : base(navigationService)
        {
            keyboardService.KeyboardIsHidden += KeyboardIsHidden;
            keyboardService.KeyboardIsShown += KeyboardIsShown;
        }

        #region -- Public properties --

        private bool _isKeyboardButtonVisible;
        public bool IsKeyboardButtonVisible
        {
            get => _isKeyboardButtonVisible;
            set => SetProperty(ref _isKeyboardButtonVisible, value);
        }

        private bool _isSignButtonsBlockVisible = true;
        public bool IsSignButtonsBlockVisible
        {
            get => _isSignButtonsBlockVisible;
            set => SetProperty(ref _isSignButtonsBlockVisible, value);
        }

        #endregion

        #region -- Private helpers --

        private void KeyboardIsHidden(object sender, System.EventArgs e)
        {
            IsSignButtonsBlockVisible = true;
            IsKeyboardButtonVisible = false;
        }

        private void KeyboardIsShown(object sender, System.EventArgs e)
        {
            IsSignButtonsBlockVisible = false;
            IsKeyboardButtonVisible = true;
        }

        #endregion
    }
}
