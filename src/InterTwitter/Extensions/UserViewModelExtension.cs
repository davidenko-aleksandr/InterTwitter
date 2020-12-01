using InterTwitter.Models;
using InterTwitter.ViewModels;

namespace InterTwitter.Extensions
{
    static class UserViewModelExtension 
    {
        public static UserModel ToUserModel(this UserViewModel viewModel)
        {
            UserModel user = null;
            if(viewModel is not null)
            {
                user = new UserModel
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

            return user;
        }
    }
}
