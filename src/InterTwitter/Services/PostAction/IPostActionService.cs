using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.ViewModels.OwlItems;
using System.Threading.Tasks;

namespace InterTwitter.Services.PostAction
{
    public interface IPostActionService
    {
        Task<AOResult<bool>> SaveActionAsync(OwlViewModel actionOwl, OwlAction action);
        Task<AOResult<bool>> ClearUserBookmarks();
    }
}
