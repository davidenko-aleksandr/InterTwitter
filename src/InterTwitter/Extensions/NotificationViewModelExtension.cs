using InterTwitter.Models;
using InterTwitter.ViewModels.NotificationItems;

namespace InterTwitter.Extensions
{
    public static class NotificationViewModelExtension
    {
        public static NotificationModel ConvertToModel(this NotificationViewModel viewModel)
        {
            var notification = new NotificationModel
            {
                Id = viewModel.Id,
                AuthorId = viewModel.AuthorId,
                OwlId = viewModel.OwlId,
                OwlText = viewModel.OwlText,
                Action = viewModel.Action,
                UserAvatar = viewModel.UserAvatar,
                UserId = viewModel.UserId,
                UserName = viewModel.UserName
            };

            return notification;
        }

    }
}
