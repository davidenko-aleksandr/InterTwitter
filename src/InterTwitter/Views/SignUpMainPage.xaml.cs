using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace InterTwitter.Views
{
    public partial class SignUpMainPage : BaseContentPage
    {
        public SignUpMainPage()
        {
            InitializeComponent();
        }

        #region -- Overrides --

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            keyboardButton.PropertyChanging += KeyboardButtonPropertyChanging;
            signButtonsBlock.PropertyChanging += SignButtonsBlockPropertyChanging;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);

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

        private void NextButtonClicked(object sender, System.EventArgs e)
        {
            emailEntry.Entry.Focus();
        }

        #endregion
    }
}
