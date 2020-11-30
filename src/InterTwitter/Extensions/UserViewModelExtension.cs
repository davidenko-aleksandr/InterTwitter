using System;
using System.Collections.Generic;
using System.Text;
using InterTwitter.Models;
using InterTwitter.ViewModels;

namespace InterTwitter.Extensions
{
    static class UserViewModelExtension 
    {
        public static UserModel ToUserModel(this UserViewModel userViewModel)
        {
            return new UserModel
                   {
                       Id = userViewModel.Id,
                       Name = userViewModel.Name,
                       Email = userViewModel.Email,
                       Password = userViewModel.Password,
                       Picture = userViewModel.Picture,
                       ProfileHeaderImage = userViewModel.ProfileHeaderImage
                   };
        }
    }
}
