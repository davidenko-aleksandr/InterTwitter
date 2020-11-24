using InterTwitter.Helpers;
using InterTwitter.ViewModels.HomePageItems;
using System.Threading.Tasks;

namespace InterTwitter.Services.Owl
{
    public interface IOwlService
    {
        Task<AOResult<OwlPictureViewModel>> GetOwlPictureAsync();

        Task<AOResult<OwlAlbumViewModel>> GetOwlTwoPicturesAsync();

        Task<AOResult<OwlSomePicturesViewModelcs>> GetOwlSomePicturesAsync();
    }
}