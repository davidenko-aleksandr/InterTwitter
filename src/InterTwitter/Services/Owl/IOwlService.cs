using InterTwitter.Helpers;
using InterTwitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.Owl
{
    public interface IOwlService
    {
        Task<AOResult<IEnumerable<OwlModel>>> GetAllOwlsAsync(string searchQuery = null);
        Task<AOResult<IEnumerable<OwlModel>>> GetAuthorOwlsAsync(int authorId);
        Task<AOResult<IEnumerable<OwlModel>>> GetSavedOwlsAsync();
        Task<AOResult<OwlModel>> GetOwlById(int owlId);
        Task<AOResult<bool>> AddOwlAsync(OwlModel owlModel);
        Task<AOResult<bool>> UpdateOwlAsync(OwlModel owl);
        Task<AOResult<IEnumerable<OwlModel>>> GetLikedOwlsAsync(int authorId);
    }
}