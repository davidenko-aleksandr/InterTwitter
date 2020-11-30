using InterTwitter.Models;
using InterTwitter.ViewModels.NotificationItems;

namespace InterTwitter.Extensions
{
    public static class NotificationModelExtension
    {
        public static NotificationViewModel ConvertToViewModel(this NotificationModel notification)
        {
            var viewModel = new NotificationViewModel
            {
                Id = notification.Id,
                AuthorId = notification.AuthorId,
                OwlId = notification.OwlId,
                OwlText = notification.OwlText,
                Action = notification.Action,
                MediaType = notification.MediaType,
                UserAvatar = notification.UserAvatar,
                UserId = notification.UserId,
                UserName = notification.UserName,
                MediaList = notification.MediaList
                
            };

            return viewModel;
        }

    }
}
