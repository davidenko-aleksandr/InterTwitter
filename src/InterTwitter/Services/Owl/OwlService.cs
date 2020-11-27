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
        private List<OwlNoMediaViewModel> _owlNoMedia;
        private List<OwlGifViewModel> _owlGif;
        private List<OwlVideoViewModel> _owlVideo;

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

                    case OwlType.NoMedia:
                        GetOwlNoMedia();
                        _owl = _owlNoMedia.First(owl => owl.AuthorId == owl.Id) as T;
                        break;

                    case OwlType.Gif:
                        GetOwlGif();
                        _owl = _owlGif.First(owl => owl.AuthorId == owl.Id) as T;
                        break;

                    case OwlType.Video:
                        GetOwlVideo();
                        _owl = _owlVideo.First(owl => owl.AuthorId == owl.Id) as T;
                        break;

                    default:
                        _owl = null;
                        break;
                }

                await Task.Delay(300);

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
                    AuthorAvatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTztRLQ_Wq4fE2jBk97nbACnuE2FEaBWKAUtg&usqp=CAU",
                    PostDate = DateTime.Now.ToString("dd.MM.yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhoto = "https://consequenceofsound.net/wp-content/uploads/2015/11/maxresdefault-1.jpg?quality=80&w=807",
                    AuthorNickName = "Rocky Balboa",
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
                    AuthorAvatar = "https://images.theconversation.com/files/350865/original/file-20200803-24-50u91u.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=1200&h=1200.0&fit=crop",
                    PostDate = DateTime.Now.ToString("dd.MM.yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhotoOne = "https://icdn.lenta.ru/images/2020/01/28/17/20200128170822958/square_320_9146846fb3b1bfae5672755bc1896214.jpg",
                    PostPhotoTwo = "https://s0.rbk.ru/v6_top_pics/media/img/7/06/755581025099067.jpeg",
                    PostPhotoThree = "https://static.toiimg.com/thumb/msid-67586673,width-800,height-600,resizemode-75,imgsize-3918697,pt-32,y_pad-40/67586673.jpg",
                    PostPhotoFour = "https://www.humanesociety.org/sites/default/files/styles/1240x698/public/2020-07/kitten-510651.jpg?h=f54c7448&itok=ZhplzyJ9",
                    PostPhotoFive = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQo4BQSpilYy5KuAptMxbOAxm4uKjFYDG6_wg&usqp=CAU",
                    PostPhotoSix = "https://img.huffingtonpost.com/asset/5e848c4825000056010586d9.jpeg?ops=1778_1000",
                    AuthorNickName = "cute cats",
                    Text = "There may be some funny text here",
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
                    AuthorAvatar = "https://stuki-druki.com/aforizms/Shakira-01.jpg",
                    PostDate = DateTime.Now.ToString("dd.MM.yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    PostPhotoOne = "https://kor.ill.in.ua/m/610x385/2457536.jpg",
                    PostPhotoTwo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ6TyjVGHZ5enIB5v4ixtwheiBzB_seknSyWQ&usqp=CAU",
                    AuthorNickName = "Shakira",
                    Text = "Wah kakaya beautiful girl...!",
                }
            };

            return _owlFewImagesMock;
        }

        private List<OwlNoMediaViewModel> GetOwlNoMedia()
        {
            _owlNoMedia = new List<OwlNoMediaViewModel>()
            {
                new OwlNoMediaViewModel()
                {
                    Id = 1,
                    AuthorId = 1,
                    AuthorAvatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTztRLQ_Wq4fE2jBk97nbACnuE2FEaBWKAUtg&usqp=CAU",
                    PostDate = DateTime.Now.ToString("dd.MM.yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    AuthorNickName = "Rocky Balboa",
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone.",
                }
            };

            return _owlNoMedia;
        }

        private List<OwlGifViewModel> GetOwlGif()
        {
            _owlGif = new List<OwlGifViewModel>()
            {
                new OwlGifViewModel()
                {
                    Id = 1,
                    AuthorId = 1,
                    AuthorAvatar = "https://images.theconversation.com/files/350865/original/file-20200803-24-50u91u.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=1200&h=1200.0&fit=crop",
                    PostDate = DateTime.Now.ToString("dd.MM.yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    AuthorNickName = "cute cats",
                    Text = "There may be some funny text here",
                    Gif = "https://i.gifer.com/Ar.gif",
                }
            };

            return _owlGif;
        }

        private List<OwlVideoViewModel> GetOwlVideo()
        {
            _owlVideo = new List<OwlVideoViewModel>()
            {
                new OwlVideoViewModel()
                {
                    Id = 1,
                    AuthorId = 1,
                    AuthorAvatar = "https://images.theconversation.com/files/350865/original/file-20200803-24-50u91u.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=1200&h=1200.0&fit=crop",
                    PostDate = DateTime.Now.ToString("dd.MM.yyyy"),
                    PostTime = DateTime.Now.ToString("HH:mm"),
                    AuthorNickName = "cute cats",
                    Text = "There may be some funny text here",
                    Video = "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4",
                }
            };

            return _owlVideo;
        }

        #endregion
    }
}
