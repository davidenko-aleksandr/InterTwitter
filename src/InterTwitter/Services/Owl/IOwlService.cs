using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.ViewModels.HomePageItems;
using System.Threading.Tasks;

namespace InterTwitter.Services.Owl
{
    public interface IOwlService
    {
        Task<AOResult<T>> GetOwlDataAsync<T>(OwlType owlType) where T : OwlViewModel, new();
    }
}