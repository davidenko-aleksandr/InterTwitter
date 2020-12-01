using System.Collections.ObjectModel;
using System.Threading.Tasks;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.HomePageItems;
using InterTwitter.Helpers;
using InterTwitter.Models;
using Prism.Navigation;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Acr.UserDialogs;
using System;
using InterTwitter.Views;
using InterTwitter.Enums;

namespace InterTwitter.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly IOwlService _owlService;
        private readonly IUserDialogs _userDialogs;

        public HomePageViewModel(
                                INavigationService navigationService,
                                IOwlService owlService,
                                IUserDialogs userDialogs)
                                : base(navigationService)
        {
            _owlService = owlService;
            _userDialogs = userDialogs;
        }

        #region -- Public properties --

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        private string _icon = "ic_home_gray";
        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private List<TestModel> _items;
        public List<TestModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private OwlViewModel selectedItem;
        public OwlViewModel SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
            }
        }

        public ICommand OpenMenuCommand => SingleExecutionCommand.FromFunc(OnOpenMenuCommandAsync);

        public ICommand OpenPostCommand => SingleExecutionCommand.FromFunc(OnOpenPostCommandAsync);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var isConnected = Connectivity.NetworkAccess;

            if (isConnected == NetworkAccess.Internet)
            {
                Icon = "ic_home_blue";

                var owls = await _owlService.GetAllOwlsAsync();

                Owls = new ObservableCollection<OwlViewModel>(owls.Result);
            }
            else
            {
                _userDialogs.Toast("No internet connection");
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            Icon = "ic_home_gray";
        }

        #endregion

        #region -- Private helpers --

        private async Task OnOpenMenuCommandAsync()
        {
            MessagingCenter.Send<object>(this, Constants.OpenMenuMessage);
        }

        private async Task OnOpenPostCommandAsync()
        {
             
            NavigationParameters parameters = new NavigationParameters { {"OwlViewModel", SelectedItem } };

            await NavigationService.NavigateAsync($"{nameof(PostPage)}", parameters);
        }

        #endregion
    }
}