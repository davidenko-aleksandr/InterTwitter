using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.ViewModels.HomePageItems;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterTwitter.Services.Owl
{
    public interface IOwlService
    {
        Task<AOResult<IEnumerable<OwlViewModel>>> GetAllOwlsAsync();
        Task<AOResult<IEnumerable<OwlViewModel>>> GetAuthorOwlsAsync(int authorId);
        Task<AOResult> AddOwlAsync(OwlModel owlModel);
    }
}