using InterTwitter.Models;
using InterTwitter.ViewModels.NotificationItems;

namespace InterTwitter.Extensions
{
    public static class NotificationViewModelExtension
    {
        public static NotificationModel ToModel(this NotificationViewModel viewModel)
        {
            NotificationModel notification = null;
            if (viewModel is not null)
            {
                notification = new NotificationModel
                {
                    Id = viewModel.Id,
                    AuthorId = viewModel.AuthorId,
                    OwlId = viewModel.OwlId,
                    OwlText = viewModel.OwlText,
                    Action = viewModel.Action,
                    MediaType = viewModel.MediaType,
                    UserAvatar = viewModel.UserAvatar,
                    UserId = viewModel.UserId,
                    UserName = viewModel.UserName,
                    MediaList = viewModel.MediaList
                };
            }
            else
            {
                //viewmodel is null
            }

            return notification;
        }

    }
}
