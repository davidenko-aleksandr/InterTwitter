using System;
using System.Windows.Input;
using Prism.Mvvm;

namespace InterTwitter.ViewModels
{
    public class MenuItemViewModel : BindableBase
    {
        public MenuItemViewModel(ICommand navigationCommand)
        {
            _navigationCommand = navigationCommand;
        }

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

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        private ICommand _navigationCommand;
        public ICommand NavigationCommand => _navigationCommand;
    }
}
