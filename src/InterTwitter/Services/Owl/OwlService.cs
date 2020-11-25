using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.ViewModels.HomePageItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.Owl
{
    public class OwlService : IOwlService
    {
        private List<OwlOneImageViewModel> _owlOneImageMock;
        private List<OwlFewImagesViewModel> _owlFewImagesMock;
        private List<OwlAlbumViewModel> _owlAlbumMock;

        public OwlService()
        {
            GetOwlOneImage();
            GetOwlAlbum();
            GetOwlFewImages();
        }

        #region -- IOwlService Implementation --

        public async Task<AOResult<T>> GetOwlDataAsync<T>(OwlType owlType) where T : OwlViewModel, new()
        {
            var result = new AOResult<T>();

            try
            {
                T _owl;

                switch (owlType)
                {
                    case OwlType.OneImage:
                        GetOwlOneImage();
                        _owl = _owlOneImageMock.First(owl => owl.AuthorId == owl.Id) as T;
                        break;

                    case OwlType.FewImages:
                        GetOwlFewImages();
                        _owl = _owlFewImagesMock.First(owl => owl.AuthorId == owl.Id) as T;
                        break;

                    case OwlType.Album:
                        GetOwlAlbum();
                        _owl = _owlAlbumMock.First(owl => owl.AuthorId == owl.Id) as T;
                        break;

                    //case OwlType.video:
                    //    break;

                    //case OwlType.Gif:
                    //    break;

                    //case OwlType.NoMedia:
                    //    break;

                    default:
                        _owl = null;
                        break;
                }
                //await Task.Delay(300);

                if (_owl != null)
                {
                    result.SetSuccess(_owl);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetOwlDataAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }


        #endregion

        #region -- Private helpers --
        private List<OwlOneImageViewModel> GetOwlOneImage()
        {
            _owlOneImageMock = new List<OwlOneImageViewModel>()
            {
                new OwlOneImageViewModel()
                {
                    Id = 1,
                    AuthorId = 1,
                    AuthorAvatar = "avatar.png",
                    PostDate = DateTime.Now.ToString("dd.MM.yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhoto = "mac_book.png",
                    AuthorNickName = "This is a Post Label",
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                }
            };

            return _owlOneImageMock;
        }

        private List<OwlAlbumViewModel> GetOwlAlbum()
        {
            _owlAlbumMock = new List<OwlAlbumViewModel>()
            {
                new OwlAlbumViewModel()
                {
                    Id = 1,
                    AuthorId = 1,
                    AuthorAvatar = "avatar.png",
                    PostDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhotoOne = "mac_book.png",
                    PostPhotoTwo = "mac_book.png",
                    PostPhotoThree = "mac_book.png",
                    PostPhotoFour = "mac_book.png",
                    PostPhotoFive = "mac_book.png",
                    PostPhotoSix = "mac_book.png",
                    AuthorNickName = "This is a Post Label",
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                }                
            };

            return _owlAlbumMock;
        }

        private List<OwlFewImagesViewModel> GetOwlFewImages()
        {
            _owlFewImagesMock = new List<OwlFewImagesViewModel>()
            {
                new OwlFewImagesViewModel()
                {
                    Id = 1,
                    AuthorId = 1,
                    AuthorAvatar = "avatar.png",
                    PostDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhotoOne = "mac_book.png",
                    PostPhotoTwo = "mac_book.png",
                    PostPhotoThree = "mac_book.png",
                    AuthorNickName = "This is a Post Label",
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                }
            };

            return _owlFewImagesMock;
        }

        #endregion
    }
}
