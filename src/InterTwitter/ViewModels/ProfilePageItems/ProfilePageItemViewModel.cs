using Prism.Mvvm;

namespace InterTwitter.ViewModels.ProfilePageItems
{
   public class ProfilePageItemViewModel : BindableBase
    {
        public ProfilePageItemViewModel(string title)
        {
            Title = title;
        }

        #region -- Public properties --
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        #endregion
    }
}
