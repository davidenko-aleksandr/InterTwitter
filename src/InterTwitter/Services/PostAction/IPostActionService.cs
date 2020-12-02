using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Threading.Tasks;

namespace InterTwitter.Services.PostAction
{
    public interface IPostActionService
    {
        Task<AOResult<bool>> SaveActionAsync(OwlModel actionOwl, OwlAction action);
        Task<AOResult<bool>> ClearUserBookmarks();
    }
}
