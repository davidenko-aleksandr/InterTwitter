using System;
using System.Windows.Input;
using Prism.Mvvm;

namespace InterTwitter.ViewModels.MediaItems
{
    public class MediaItemViewModel : BindableBase
    {
        public MediaItemViewModel(string mediaPath, ICommand removeItemCommand)
        {
            MediaPath = mediaPath;
            RemoveItemCommand = removeItemCommand;
        }

        private string _mediaPath;
        public string MediaPath
        {
            get => _mediaPath;
            set => SetProperty(ref _mediaPath, value);
        }

        private ICommand _removeItemCommand;
        public ICommand RemoveItemCommand
        {
            get => _removeItemCommand;
            set => SetProperty(ref _removeItemCommand, value);
        }
    }
}
