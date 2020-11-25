using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class SignUpPasswordPage : BaseContentPage
    {
        public SignUpPasswordPage()
        {
            InitializeComponent();

            keyboardButton.PropertyChanging += KeyboardButtonPropertyChanging;
            signButtonsBlock.PropertyChanging += SignButtonsBlockPropertyChanging;
        }


        #region -- Private helpers -- 

        private void KeyboardButtonPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IsVisible)))
            {
                if (keyboardButton.IsVisible)
                {
                    keyboardButton.FadeTo(300, 1);
                }
                else
                {
                    keyboardButton.Opacity = 0;
                    keyboardButton.FadeTo(1, 300);
                }
            }
        }

        private void SignButtonsBlockPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IsVisible)))
            {
                if (signButtonsBlock.IsVisible)
                {
                    signButtonsBlock.FadeTo(300, 1);
                }
                else
                {
                    signButtonsBlock.Opacity = 0;
                    signButtonsBlock.FadeTo(1, 300);
                }
            }
        }

        #endregion
    }
}
