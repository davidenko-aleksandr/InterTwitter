using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.ViewModels.NotificationItems;
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
