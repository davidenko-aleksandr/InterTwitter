using InterTwitter.Models;
using InterTwitter.ViewModels;

namespace InterTwitter.Extensions
{
    static class UserViewModelExtension 
    {
        public static UserModel ConvertToUserModel(this UserViewModel userViewModel)
        {
            var user = new UserModel
            {
                Id = userViewModel.Id,
                Name = userViewModel.Name,
                Email = userViewModel.Email,
                Password = userViewModel.Password,
                Avatar = userViewModel.Avatar,
                ProfileHeaderImage = userViewModel.ProfileHeaderImage
            };

            return user;
        }
    }
}
