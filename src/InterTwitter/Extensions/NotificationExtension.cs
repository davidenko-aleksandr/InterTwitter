using InterTwitter.Models;
using InterTwitter.ViewModels.NotificationItems;
using System.Windows.Input;

namespace InterTwitter.Extensions
{
    public static class NotificationExtension
    {
        public static NotificationViewModel ToViewModel(this NotificationModel model, ICommand openPostCommand)
        {
            NotificationViewModel notification = null;
            if (model is not null)
            {
                notification = new NotificationViewModel
                {
                    Id = model.Id,
                    Author = model.Author.ToViewModel(),
                    Owl = model.Owl.ToViewModel(model.Author.Id, null, null, null),
                    Action = model.Action,
                    User = model.User.ToViewModel(),
                    ItemTappedCommand = openPostCommand
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
