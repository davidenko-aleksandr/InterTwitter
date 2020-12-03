using InterTwitter.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.ViewModels
{
   public class UserViewModel : BindableBase
    {
        public UserViewModel(UserModel userModel)
        {
            Id = userModel.Id;
            Name = userModel.Name;
            Email = userModel.Email;
            Password = userModel.Password;
            Avatar = userModel.Avatar;
            ProfileHeaderImage = userModel.ProfileHeaderImage;
        }

        #region -- Public properties --

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _avatar;
        public string Avatar
        {
            get => _avatar;
            set => SetProperty(ref _avatar, value);
        }

        private string _profileHeaderImage;
        public string ProfileHeaderImage
        {
            get => _profileHeaderImage;
            set => SetProperty(ref _profileHeaderImage, value);
        }

        #endregion

    }
}
