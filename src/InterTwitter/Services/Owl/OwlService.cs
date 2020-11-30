using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.UserService;
using InterTwitter.ViewModels.HomePageItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.Owl
{
    public class OwlService : IOwlService
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;

        private List<OwlModel> _owlsMock;

        public OwlService(IUserService userService,
                          IAuthorizationService authorizationService)
        {
            _userService = userService;
            _authorizationService = authorizationService;

            InitMock();
        }

        #region -- IOwlService Implementation --

        public async Task<AOResult> AddOwlAsync(OwlModel owlModel)
        {
            var result = new AOResult();

            try
            {
                if (owlModel is not null)
                {
                    var res =  await _authorizationService.GetAuthorizedUserAsync();
                    var author = res.Result;

                    owlModel.Id = _owlsMock.Count;
                    owlModel.AuthorId = author.Id;

                    _owlsMock.Add(owlModel);
                    result.SetSuccess();
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllOwlsAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<OwlViewModel>>> GetAllOwlsAsync()
        {
            var result = new AOResult<IEnumerable<OwlViewModel>>();

            try
            {
                List<OwlViewModel> owls = new List<OwlViewModel>();

                foreach (OwlModel owl in _owlsMock)
                {
                    OwlViewModel owlVM = null;

                    var res = await _userService.GetUserAsync(owl.AuthorId);
                    var author = res.Result;

                    switch (owl.MediaType)
                    {
                        case OwlType.OneImage:
                            {
                                owls.Add(owlVM = new OwlOneImageViewModel(owl, author));
                                break;
                            }

                        case OwlType.FewImages:
                            {
                                owls.Add(owlVM = new OwlFewImagesViewModel(owl, author));
                                break;
                            }

                        case OwlType.Gif:
                            {
                                owls.Add(owlVM = new OwlGifViewModel(owl, author));
                                break;
                            }

                        case OwlType.Video:
                            {
                                owls.Add(owlVM = new OwlVideoViewModel(owl, author));
                                break;
                            }

                        case OwlType.NoMedia:
                            {
                                owls.Add(owlVM = new OwlNoMediaViewModel(owl, author));
                                break;
                            }

                        default:
                            break;
                    }
                }

                await Task.Delay(300);

                if (owls is not null)
                {
                    result.SetSuccess(owls);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllOwlsAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<OwlViewModel>>> GetAuthorOwlsAsync(int authorId)
        {
            var result = new AOResult<IEnumerable<OwlViewModel>>();

            try
            {
                var res = await GetAllOwlsAsync();
                var userRes = await _authorizationService.GetAuthorizedUserAsync();
                var authorizedUser = userRes.Result;
                var owls = res.Result.Where(x => x.AuthorId == authorizedUser.Id);

                if (owls is not null)
                {
                    result.SetSuccess(owls);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAllOwlsAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Private helpers --

        private void InitMock()
        {
            _owlsMock = new List<OwlModel>();

            var owlModel = new OwlModel
            {
                Id = _owlsMock.Count,
                AuthorId = 1,
                Date = DateTime.Now,
                Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                MediaType = OwlType.OneImage,
                Media = new List<string>()
                {
                    "https://consequenceofsound.net/wp-content/uploads/2015/11/maxresdefault-1.jpg?quality=80&w=807",
                }
            };

            _owlsMock.Add(owlModel);

            owlModel = new OwlModel
            {
                Id = _owlsMock.Count,
                AuthorId = 0,
                Date = DateTime.Now,
                Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                MediaType = OwlType.FewImages,
                Media = new List<string>()
                {
                    "https://icdn.lenta.ru/images/2020/01/28/17/20200128170822958/square_320_9146846fb3b1bfae5672755bc1896214.jpg",
                    "https://s0.rbk.ru/v6_top_pics/media/img/7/06/755581025099067.jpeg",
                    "https://static.toiimg.com/thumb/msid-67586673,width-800,height-600,resizemode-75,imgsize-3918697,pt-32,y_pad-40/67586673.jpg",
                    "https://www.humanesociety.org/sites/default/files/styles/1240x698/public/2020-07/kitten-510651.jpg?h=f54c7448&itok=ZhplzyJ9",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQo4BQSpilYy5KuAptMxbOAxm4uKjFYDG6_wg&usqp=CAU",
                    "https://img.huffingtonpost.com/asset/5e848c4825000056010586d9.jpeg?ops=1778_1000",

                }
            };

            _owlsMock.Add(owlModel);

            owlModel = new OwlModel
            {
                Id = _owlsMock.Count,
                AuthorId = 2,
                Date = DateTime.Now,
                Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                MediaType = OwlType.FewImages,
                Media = new List<string>()
                {
                    "https://kor.ill.in.ua/m/610x385/2457536.jpg",
                    "https://data.whicdn.com/images/50617398/original.jpg",

                }
            };

            _owlsMock.Add(owlModel);

            owlModel = new OwlModel
            {
                Id = _owlsMock.Count,
                AuthorId = 1,
                Date = DateTime.Now,
                Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                MediaType = OwlType.Gif,
                Media = new List<string>()
                {
                    "https://i.gifer.com/Ar.gif",
                }
            };

            _owlsMock.Add(owlModel);

            owlModel = new OwlModel
            {
                Id = _owlsMock.Count,
                AuthorId = 1,
                Date = DateTime.Now,
                Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                MediaType = OwlType.Video,
                Media = new List<string>()
                {
                    "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4",
                }
            };

            _owlsMock.Add(owlModel);

            owlModel = new OwlModel
            {
                Id = _owlsMock.Count,
                AuthorId = 1,
                Date = DateTime.Now,
                Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone.",
                MediaType = OwlType.NoMedia,
            };

            _owlsMock.Add(owlModel);

            _owlsMock.OrderBy(x => x.Date);
        }

        #endregion
    }
}
