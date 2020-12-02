using System;
using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class AddPostPage : BaseContentPage
    {
        public AddPostPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            editor.Focused += InputFocused;
            editor.Unfocused += InputUnfocused;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            editor.Focused -= InputFocused;
            editor.Unfocused -= InputUnfocused;
        }

        void InputFocused(object sender, EventArgs args)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                mediaBar.Margin = new Thickness(0, 0, 0, 350);
            }
        }

        void InputUnfocused(object sender, EventArgs args)
        {
            mediaBar.Margin = new Thickness(0, 0, 0, 0);
        }
    }
}
