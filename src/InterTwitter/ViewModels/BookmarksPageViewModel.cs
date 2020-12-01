using Prism.Navigation;

namespace InterTwitter.ViewModels
{
    public class BookmarksPageViewModel : BaseViewModel
    {
        
        public BookmarksPageViewModel(INavigationService navigationService)
                                    : base(navigationService)
        {
        }

        #region -- Public Properties --

        private string _icon = "ic_bookmarks_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_bookmarks_blue";
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_bookmarks_gray";
        }

        #endregion
    }
}
