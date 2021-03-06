﻿using InterTwitter.Models;
using InterTwitter.ViewModels;

namespace InterTwitter.Extensions
{
    public static class UserExtension
    {
        public static UserModel ToModel(this UserViewModel viewModel)
        {
            UserModel model = null;
            if (viewModel != null)
            {
                model = new UserModel
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Password = viewModel.Password,
                    Avatar = viewModel.Avatar,
                    ProfileHeaderImage = viewModel.ProfileHeaderImage
                };
            }
            else
            {
                //viewmodel is null
            }

            return model;
        }

        public static UserViewModel ToViewModel(this UserModel model)
        {
            UserViewModel viewModel = null;
            if (model != null)
            {
                viewModel = new UserViewModel(model);
            }
            else
            {
                //viewmodel is null
            }

            return viewModel;
        }
    }
}
