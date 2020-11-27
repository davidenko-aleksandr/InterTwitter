using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class LogInPage : BaseContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        #region -- Overrides --

        protected override void OnAppearing()
        {
            base.OnAppearing();

            keyboardButton.PropertyChanging += KeyboardButtonPropertyChanging;
            signButtonsBlock.PropertyChanging += SignButtonsBlockPropertyChanging;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            keyboardButton.PropertyChanging -= KeyboardButtonPropertyChanging;
            signButtonsBlock.PropertyChanging -= SignButtonsBlockPropertyChanging;
        }

        #endregion

        #region -- Private helpers -- 

        private void KeyboardButtonPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IsVisible)))
            {
                if (keyboardButton.IsVisible)
                {
                    keyboardButton.FadeTo(0);
                }
                else
                {
                    keyboardButton.Opacity = 0;
                    keyboardButton.FadeTo(1);
                }
            }
        }

        private void SignButtonsBlockPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IsVisible)))
            {
                if (signButtonsBlock.IsVisible)
                {
                    signButtonsBlock.FadeTo(0);
                }
                else
                {
                    signButtonsBlock.Opacity = 0;
                    signButtonsBlock.FadeTo(1);
                }
            }
        }

        #endregion

        private void NextButtonClicked(object sender, System.EventArgs e)
        {
            passwordEntry.Entry.Focus();
        }
    }
}
