﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using Prism.Navigation;
using Xamarin.Forms;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel(INavigationService navigationService)
                                : base(navigationService)
        {
            FillTheList();
        }

        private List<TestModel> _items;
        public List<TestModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private void FillTheList()
        {
            var collection = new List<TestModel>();

            for(int i = 0; i < 20; i++)
            {
                collection.Add(new TestModel() { Content = (i + 1).ToString() });
            }

            Items = collection;
        }

        #region -- Public Properties --

        private string _icon = "ic_home_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public ICommand OpenMenuCommand => SingleExecutionCommand.FromFunc(OnOpenMenuCommandAsync);

        private async Task OnOpenMenuCommandAsync()
        {
            MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
        }

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Icon = "ic_home_blue";
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_home_gray";
        }

        #endregion
    }
}
