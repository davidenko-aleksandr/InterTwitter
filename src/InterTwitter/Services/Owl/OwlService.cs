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
        private List<OwlPictureViewModel> _owlPicturesMock;
        private List<OwlSomePicturesViewModelcs> _owlSomePicturesMock;
        private List<OwlAlbumViewModel> _owlTwoPicturesMock;

        public OwlService()
        {
            GetOwlPicture();
            GetOwlTwoPictures();
            GetOwlSomePictures();
        }

        #region -- IOwlService Implementation --

        public async Task<AOResult<OwlPictureViewModel>> GetOwlPictureAsync()
        {
            var result = new AOResult<OwlPictureViewModel>();

            try
            {
                var owl = _owlPicturesMock.First(owl => owl.OwlId == owl.UserId);

                await Task.Delay(300);

                if (owl != null)
                {
                    result.SetSuccess(owl);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetOwlPictureAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<OwlAlbumViewModel>> GetOwlTwoPicturesAsync()
        {
            var result = new AOResult<OwlAlbumViewModel>();

            try
            {
                var owl = _owlTwoPicturesMock.First(owl => owl.OwlId == owl.UserId);

                await Task.Delay(300);

                if (owl != null)
                {
                    result.SetSuccess(owl);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetOwlTwoPicturesAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<OwlSomePicturesViewModelcs>> GetOwlSomePicturesAsync()
        {
            var result = new AOResult<OwlSomePicturesViewModelcs>();

            try
            {
                var owl = _owlSomePicturesMock.First(owl => owl.OwlId == owl.UserId);

                await Task.Delay(300);

                if (owl != null)
                {
                    result.SetSuccess(owl);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetOwlTwoPicturesAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }


        #endregion

        #region -- Private helpers --
        private void GetOwlPicture()
        {
            _owlPicturesMock = new List<OwlPictureViewModel>()
            {
                new OwlPictureViewModel()
                {
                    UserId = 1,
                    OwlId = 1,
                    AuthorAvatar = "avatar.png",
                    PostDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhoto = "mac_book.png",
                    PostLabel = "This is a Post Label",
                    PostDescription = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                }
            };
        }

        private void GetOwlTwoPictures()
        {
            _owlTwoPicturesMock = new List<OwlAlbumViewModel>()
            {
                new OwlAlbumViewModel()
                {
                    UserId = 1,
                    OwlId = 1,
                    AuthorAvatar = "avatar.png",
                    PostDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhotoOne = "mac_book.png",
                    PostPhotoTwo = "mac_book.png",
                    PostLabel = "This is a Post Label",
                    PostDescription = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                }
            };
        }

        private void GetOwlSomePictures()
        {
            _owlSomePicturesMock = new List<OwlSomePicturesViewModelcs>()
            {
                new OwlSomePicturesViewModelcs()
                {
                    UserId = 1,
                    OwlId = 1,
                    AuthorAvatar = "avatar.png",
                    PostDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhotoOne = "mac_book.png",
                    PostPhotoTwo = "mac_book.png",
                    PostLabel = "This is a Post Label",
                    PostDescription = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                }
            };
        }

        #endregion
    }
}
