using System;
using System.Windows.Input;
using Prism.Mvvm;

namespace InterTwitter.ViewModels
{
    public class MenuItemViewModel : BindableBase
    {
        #region -- Public properties --

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _icon;
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private Type _pageType;
        public Type PageType
        {
            get => _pageType;
            set => SetProperty(ref _pageType, value);
        }

        private ICommand _navigationCommand;
        public ICommand NavigationCommand
        {
            get => _navigationCommand;
            set => SetProperty(ref _navigationCommand, value);
        }

        #endregion

    }
}
