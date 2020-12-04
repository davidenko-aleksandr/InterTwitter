using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class SearchPage : BaseContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        #region -- Overrides --

        protected override void OnAppearing()
        {
            base.OnAppearing();

            popularThemes.PropertyChanging += PopularThemesPropertyChanging;
            foundPosts.PropertyChanging += FoundPostsPropertyChanging;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            popularThemes.PropertyChanging -= PopularThemesPropertyChanging;
            foundPosts.PropertyChanging -= FoundPostsPropertyChanging;
        }

        #endregion

        #region -- Private helpers --

        private void PopularThemesPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IsVisible)))
            {
                if (popularThemes.IsVisible)
                {
                    popularThemes.FadeTo(0, 100);
                }
                else
                {
                    popularThemes.Opacity = 0;
                    popularThemes.FadeTo(1, 100);
                }
            }
        }

        private void FoundPostsPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(IsVisible)))
            {
                if (foundPosts.IsVisible)
                {
                    foundPosts.FadeTo(0, 100);
                }
                else
                {
                    foundPosts.Opacity = 0;
                    foundPosts.FadeTo(1, 100);
                }
            }
        }

        #endregion
    }
}
