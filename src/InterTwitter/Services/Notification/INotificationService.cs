using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.Notification
{
    public interface INotificationService
    {
        Task<AOResult<bool>> AddNotificationAsync(OwlModel actionOwl, OwlAction action);

        Task<AOResult<IEnumerable<NotificationModel>>> GetNotificationCollectionAsync();
    }
}
