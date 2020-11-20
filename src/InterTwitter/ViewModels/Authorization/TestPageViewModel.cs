using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.ViewModels.Authorization
{
    public class TestPageViewModel : BaseViewModel
    {
        public TestPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        private ICommand _selectEntryCommand;
        public ICommand SelectEntryCommand => _selectEntryCommand ??= new Command<Entry>(OnSelectEntryCommand);

        private void OnSelectEntryCommand(Entry obj)
        {
            var result = obj;
        }
    }
}