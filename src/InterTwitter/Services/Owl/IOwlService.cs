using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.ViewModels.OwlItems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.Owl
{
    public interface IOwlService
    {
        Task<AOResult<IEnumerable<OwlViewModel>>> GetAllOwlsAsync();
        Task<AOResult<IEnumerable<OwlViewModel>>> GetAuthorOwlsAsync(int authorId);
        Task<AOResult<bool>> AddOwlAsync(OwlModel owlModel);
        Task<AOResult<bool>> UpdateOwlAsync(OwlViewModel owl);
    }
}