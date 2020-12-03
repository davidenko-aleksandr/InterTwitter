using InterTwitter.Models;
using InterTwitter.ViewModels.NotificationItems;

namespace InterTwitter.Extensions
{
    public static class NotificationModelExtension
    {
        public static NotificationViewModel ToViewModel(this NotificationModel model)
        {
            NotificationViewModel notification = null;
            if (model is not null)
            {
                notification = new NotificationViewModel
                {
                    Id = model.Id,
                    AuthorId = model.AuthorId,
                    OwlId = model.OwlId,
                    OwlText = model.OwlText,
                    Action = model.Action,
                    MediaType = model.MediaType,
                    UserAvatar = model.UserAvatar,
                    UserId = model.UserId,
                    UserName = model.UserName,
                    MediaList = model.MediaList,
                };
            }
            else
            {
                //model is null
            }

            return notification;
        }
    }
}
