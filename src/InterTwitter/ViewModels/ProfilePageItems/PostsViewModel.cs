using Acr.UserDialogs;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using InterTwitter.ViewModels.HomePageItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;

namespace InterTwitter.ViewModels.ProfilePageItems
{
    class PostsViewModel : PofilePageItemViewModel
    {
        
        public PostsViewModel(string title, ObservableCollection<OwlViewModel> owls) : base(title)
        {

            Owls = owls;
           
        }

        #region -- Public properties --

        private UserViewModel _user;
        public UserViewModel User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private ObservableCollection<OwlViewModel> _owls;
        public ObservableCollection<OwlViewModel> Owls
        {
            get => _owls;
            set => SetProperty(ref _owls, value);
        }

        #endregion

        #region -- Private properties --

        //private async void InitDataAsync()
        //{           
        //    var isConnected = Connectivity.NetworkAccess;

        //    if (isConnected == NetworkAccess.Internet)
        //    {
        //        var user = await _authorizationService.GetAuthorizedUserAsync();
        //        User = user.Result;

        //        var owls = await _owlService.GetAuthorOwlsAsync(User.Id);

        //        Owls = new ObservableCollection<OwlViewModel>(owls.Result);
        //    }
        //    else
        //    {
        //        // IUserDialogs.Toast("No internet connection");
        //    }
        //}

        #endregion
    }
}
