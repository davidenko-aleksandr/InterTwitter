using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.ViewModels.NotificationItems;
using InterTwitter.ViewModels.OwlItems;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace InterTwitter.Services.Notification
{
    public interface INotificationService
    {
        Task<AOResult<bool>> AddNotificationAsync(OwlViewModel actionOwl, OwlAction action);

        Task<AOResult<ObservableCollection<NotificationViewModel>>> GetNotificationCollectionAsync();
    }
}
