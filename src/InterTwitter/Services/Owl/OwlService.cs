using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using InterTwitter.ViewModels.OwlItems;
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
        private readonly ISettingsService _settingsService;

        private List<OwlModel> _owlsMock;

        public OwlService(IUserService userService,
                          IAuthorizationService authorizationService,
                          ISettingsService settingsService)
        {
            _userService = userService;
            _authorizationService = authorizationService;
            _settingsService = settingsService;

            InitMock();
        }

        #region -- IOwlService Implementation --

        public async Task<AOResult<bool>> AddOwlAsync(OwlModel owlModel)
        {
            var result = new AOResult<bool>();

            try
            {
                if (owlModel is not null)
                {
                    var userResult =  await _authorizationService.GetAuthorizedUserAsync();
                    if (userResult.IsSuccess)
                    {
                        var author = userResult.Result;

                        owlModel.Id = _owlsMock.Count;
                        owlModel.AuthorId = author.Id;

                        _owlsMock.Add(owlModel);

                        result.SetSuccess(true);
                    }
                    else
                    {
                        result.SetFailure();
                    }

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

        public async Task<AOResult<IEnumerable<OwlViewModel>>> GetAllOwlsAsync(string searchQuery = null)
        {
            var result = new AOResult<IEnumerable<OwlViewModel>>();

            try
            {
                List<OwlViewModel> owls = new List<OwlViewModel>();

                foreach (OwlModel owl in _owlsMock)
                {
                    OwlViewModel owlVM = null;

                    var usersResult = await _userService.GetUserAsync(owl.AuthorId);
                    var author = usersResult.Result;

                    switch (owl.MediaType)
                    {
                        case OwlType.OneImage:
                            {
                                owls.Add(owlVM = new OwlOneImageViewModel(owl, author, _settingsService.AuthorizedUserId));
                                break;
                            }

                        case OwlType.FewImages:
                            {
                                owls.Add(owlVM = new OwlFewImagesViewModel(owl, author, _settingsService.AuthorizedUserId));
                                break;
                            }

                        case OwlType.Video:
                            {
                                owls.Add(owlVM = new OwlVideoViewModel(owl, author, _settingsService.AuthorizedUserId));
                                break;
                            }

                        case OwlType.NoMedia:
                            {
                                owls.Add(owlVM = new OwlNoMediaViewModel(owl, author, _settingsService.AuthorizedUserId));
                                break;
                            }

                        default:
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    owls = new List<OwlViewModel>(owls.Where(x => 
                    x.AuthorNickName.ToUpper().Contains(searchQuery?.ToUpper()) ||
                    x.Text.ToUpper().Contains(searchQuery?.ToUpper())));
                }
                else
                {
                    //searchQuery is not given
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

        public async Task<AOResult<bool>> ClearUserBookmarks()
        {
            var result = new AOResult<bool>();

            try
            {
                var owlResult = await GetSavedOwlsAsync();
                var userResult = await _authorizationService.GetAuthorizedUserAsync();

                if (userResult.IsSuccess && owlResult.IsSuccess)
                {
                    var authorizedUser = userResult.Result;

                    var owls = owlResult.Result;

                    foreach(var item in owls)
                    {
                        item.SavesList.Remove(authorizedUser.Id);
                    }

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

        public async Task<AOResult<IEnumerable<OwlViewModel>>> GetSavedOwlsAsync()
        {
            var result = new AOResult<IEnumerable<OwlViewModel>>();

            try
            {
                var owlResult = await GetAllOwlsAsync();
                var userResult = await _authorizationService.GetAuthorizedUserAsync();

                if (userResult.IsSuccess && owlResult.IsSuccess)
                {
                    var authorizedUser = userResult.Result;

                    var savedOwls = owlResult.Result.Where(x => x.SavesList.Contains(authorizedUser.Id));

                    result.SetSuccess(savedOwls);
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
                var owlsResult = await GetAllOwlsAsync();
                if (owlsResult.IsSuccess)
                {
                    var owls = owlsResult.Result.Where(x => x.AuthorId == authorId);

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

        public async Task<AOResult<bool>> UpdateOwlAsync(OwlViewModel owl)
        {
            var result = new AOResult<bool>();

            try
            {
                var changingOwl = _owlsMock.FirstOrDefault(x => x.Id == owl.Id);

                if (changingOwl is not null)
                {
                    changingOwl.AuthorId = owl.AuthorId;
                    changingOwl.LikesList = owl.LikesList;
                    changingOwl.Media = owl.Media;
                    changingOwl.MediaType = owl.MediaType;
                    changingOwl.SavesList = owl.SavesList;
                    changingOwl.Text = owl.Text;

                    result.SetSuccess(true);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(UpdateOwlAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Private helpers --

        private async Task InitMock()
        {
            _owlsMock = new List<OwlModel>()
            {
                new OwlModel
                {
                    Id = 0,
                    AuthorId = 0,
                    Date = new DateTime(2020, 11, 28, 21, 48, 0),
                    Text = $"FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse!",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 1,
                    AuthorId = 0,
                    Date = new DateTime(2020, 11, 20, 12, 00, 00),
                    Text = "Look at this in Michigan! A day AFTER the election, Biden receives a dump of 134,886 votes at 6:31AM!",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnOyo7eXcAURaz6?format=jpg&name=large"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 2,
                    AuthorId = 0,
                    Date = new DateTime(2019, 3, 1, 15, 40, 00),
                    Text = "So true!",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 3,
                    AuthorId = 2,
                    Date = DateTime.Now,
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://i.gifer.com/Ar.gif"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 4,
                    AuthorId = 3,
                    Date = new DateTime(2020, 11, 25, 18, 30, 25),
                    Text = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnsDBAZW4AAov-H?format=jpg&name=large"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 5,
                    AuthorId = 3,
                    Date = new DateTime(2020, 8, 14, 18, 30, 25),
                    Text = $"Measure, optimize, and fine-tune the #performance of your #Android apps with #Xamarin & #XamarinForms with these tips and tricks by JonathanPeppers",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EfZTe2JWoAAsUuE?format=png&name=small",
                        "https://pbs.twimg.com/media/Ee1a-gVXgAEZcFT?format=png&name=small"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 6,
                    AuthorId = 3,
                    Date = new DateTime(2020, 8, 5, 18, 30, 25),
                    Text = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://uc.uxpin.com/files/614612/609600/www.GIFCreator.me_W6dTbP.gif"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 7,
                    AuthorId = 3,
                    Date = new DateTime(2020, 7, 2, 18, 30, 25),
                    Text = $"This guest post by Charlin Agramonte elaborates on how multilingual support is one of the most common requirements for mobile apps and the simplicity of building mobile apps with #Xamarin that handle multiple languages.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 8,
                    AuthorId = 3,
                    Date = new DateTime(2020, 3, 4, 18, 30, 25),
                    Text = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "http://vjs.zencdn.net/v/oceans.mp4"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 9,
                    AuthorId = 2,
                    Date = new DateTime(2019, 11, 6, 18, 30, 25),
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>()
                },
                new OwlModel
                {
                    Id = 10,
                    AuthorId = 3,
                    Date = new DateTime(2012, 3, 4, 18, 30, 25),
                    Text = $"In the latest Xamarin Community Standup, join the Xamarin team as they discuss the latest and greatest for Xamarin. This week we sit down with Eilon Lipton to discuss the latest in the Mobile #Blazor Bindings. #XamarinForms #Blazor",
                    MediaType = OwlType.FewImages,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/Emuf9aiXEAcjdXw?format=jpg&name=small",
                        "https://pbs.twimg.com/media/Empy-qvWEAIz7CT?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmplPyCW8AAL9g7?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmoZtwnW8AIbaO2?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmfLMz9WEAAinng?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmeURLUWMAgBJb_?format=jpg&name=small"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },                
                new OwlModel
                {
                    Id = 11,
                    AuthorId = 0,
                    Date = new DateTime(2020, 11, 20, 12, 00, 00),
                    Text = "Look at this in Michigan! A day AFTER the election, Biden receives a dump of 134,886 votes at 6:31AM!",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnOyo7eXcAURaz6?format=jpg&name=large"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 12,
                    AuthorId = 0,
                    Date = new DateTime(2019, 3, 1, 15, 40, 00),
                    Text = "So true!",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1329535287735816194"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 13,
                    AuthorId = 2,
                    Date = DateTime.Now,
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 14,
                    AuthorId = 3,
                    Date = new DateTime(2020, 11, 25, 18, 30, 25),
                    Text = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnsDBAZW4AAov-H?format=jpg&name=large"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 15,
                    AuthorId = 3,
                    Date = new DateTime(2020, 8, 14, 18, 30, 25),
                    Text = $"Measure, optimize, and fine-tune the #performance of your #Android apps with #Xamarin & #XamarinForms with these tips and tricks by JonathanPeppers",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EfZTe2JWoAAsUuE?format=png&name=small",
                        "https://pbs.twimg.com/media/Ee1a-gVXgAEZcFT?format=png&name=small"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 16,
                    AuthorId = 3,
                    Date = new DateTime(2020, 8, 5, 18, 30, 25),
                    Text = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 17,
                    AuthorId = 3,
                    Date = new DateTime(2020, 7, 2, 18, 30, 25),
                    Text = $"This guest post by Charlin Agramonte elaborates on how multilingual support is one of the most common requirements for mobile apps and the simplicity of building mobile apps with #Xamarin that handle multiple languages.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 18,
                    AuthorId = 3,
                    Date = new DateTime(2020, 3, 4, 18, 30, 25),
                    Text = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1262782634335383554"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 19,
                    AuthorId = 2,
                    Date = new DateTime(2019, 11, 6, 18, 30, 25),
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 20,
                    AuthorId = 3,
                    Date = new DateTime(2012, 3, 4, 18, 30, 25),
                    Text = $"In the latest Xamarin Community Standup, join the Xamarin team as they discuss the latest and greatest for Xamarin. This week we sit down with Eilon Lipton to discuss the latest in the Mobile #Blazor Bindings. #XamarinForms #Blazor",
                    MediaType = OwlType.FewImages,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/Emuf9aiXEAcjdXw?format=jpg&name=small",
                        "https://pbs.twimg.com/media/Empy-qvWEAIz7CT?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmplPyCW8AAL9g7?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmoZtwnW8AIbaO2?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmfLMz9WEAAinng?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmeURLUWMAgBJb_?format=jpg&name=small"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 21,
                    AuthorId = 0,
                    Date = new DateTime(2020, 11, 28, 21, 48, 0),
                    Text = $"FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse!",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 22,
                    AuthorId = 0,
                    Date = new DateTime(2020, 11, 20, 12, 00, 00),
                    Text = "Look at this in Michigan! A day AFTER the election, Biden receives a dump of 134,886 votes at 6:31AM!",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnOyo7eXcAURaz6?format=jpg&name=large"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 23,
                    AuthorId = 0,
                    Date = new DateTime(2019, 3, 1, 15, 40, 00),
                    Text = "So true!",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1329535287735816194"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 24,
                    AuthorId = 2,
                    Date = DateTime.Now,
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 25,
                    AuthorId = 3,
                    Date = new DateTime(2020, 11, 25, 18, 30, 25),
                    Text = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnsDBAZW4AAov-H?format=jpg&name=large"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 26,
                    AuthorId = 3,
                    Date = new DateTime(2020, 8, 14, 18, 30, 25),
                    Text = $"Measure, optimize, and fine-tune the #performance of your #Android apps with #Xamarin & #XamarinForms with these tips and tricks by JonathanPeppers",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EfZTe2JWoAAsUuE?format=png&name=small",
                        "https://pbs.twimg.com/media/Ee1a-gVXgAEZcFT?format=png&name=small"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 27,
                    AuthorId = 3,
                    Date = new DateTime(2020, 8, 5, 18, 30, 25),
                    Text = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 28,
                    AuthorId = 3,
                    Date = new DateTime(2020, 7, 2, 18, 30, 25),
                    Text = $"This guest post by Charlin Agramonte elaborates on how multilingual support is one of the most common requirements for mobile apps and the simplicity of building mobile apps with #Xamarin that handle multiple languages.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 29,
                    AuthorId = 3,
                    Date = new DateTime(2020, 3, 4, 18, 30, 25),
                    Text = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1262782634335383554"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 30,
                    AuthorId = 2,
                    Date = new DateTime(2019, 11, 6, 18, 30, 25),
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 31,
                    AuthorId = 3,
                    Date = new DateTime(2012, 3, 4, 18, 30, 25),
                    Text = $"In the latest Xamarin Community Standup, join the Xamarin team as they discuss the latest and greatest for Xamarin. This week we sit down with Eilon Lipton to discuss the latest in the Mobile #Blazor Bindings. #XamarinForms #Blazor",
                    MediaType = OwlType.FewImages,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/Emuf9aiXEAcjdXw?format=jpg&name=small",
                        "https://pbs.twimg.com/media/Empy-qvWEAIz7CT?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmplPyCW8AAL9g7?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmoZtwnW8AIbaO2?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmfLMz9WEAAinng?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmeURLUWMAgBJb_?format=jpg&name=small"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 32,
                    AuthorId = 0,
                    Date = new DateTime(2020, 11, 28, 21, 48, 0),
                    Text = $"FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse!",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 33,
                    AuthorId = 0,
                    Date = new DateTime(2020, 11, 20, 12, 00, 00),
                    Text = "Look at this in Michigan! A day AFTER the election, Biden receives a dump of 134,886 votes at 6:31AM!",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnOyo7eXcAURaz6?format=jpg&name=large"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 34,
                    AuthorId = 0,
                    Date = new DateTime(2019, 3, 1, 15, 40, 00),
                    Text = "So true!",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1329535287735816194"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 35,
                    AuthorId = 2,
                    Date = DateTime.Now,
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 36,
                    AuthorId = 3,
                    Date = new DateTime(2020, 11, 25, 18, 30, 25),
                    Text = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnsDBAZW4AAov-H?format=jpg&name=large"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 37,
                    AuthorId = 3,
                    Date = new DateTime(2020, 8, 14, 18, 30, 25),
                    Text = $"Measure, optimize, and fine-tune the #performance of your #Android apps with #Xamarin & #XamarinForms with these tips and tricks by JonathanPeppers",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EfZTe2JWoAAsUuE?format=png&name=small",
                        "https://pbs.twimg.com/media/Ee1a-gVXgAEZcFT?format=png&name=small"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 38,
                    AuthorId = 3,
                    Date = new DateTime(2020, 8, 5, 18, 30, 25),
                    Text = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 39,
                    AuthorId = 3,
                    Date = new DateTime(2020, 7, 2, 18, 30, 25),
                    Text = $"This guest post by Charlin Agramonte elaborates on how multilingual support is one of the most common requirements for mobile apps and the simplicity of building mobile apps with #Xamarin that handle multiple languages.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 40,
                    AuthorId = 3,
                    Date = new DateTime(2020, 3, 4, 18, 30, 25),
                    Text = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild #microsoft",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1262782634335383554"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 41,
                    AuthorId = 2,
                    Date = new DateTime(2019, 11, 6, 18, 30, 25),
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone. #StalloneRules #Number1 #sports",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 42,
                    AuthorId = 3,
                    Date = new DateTime(2012, 3, 4, 18, 30, 25),
                    Text = $"In the latest Xamarin Community Standup, join the Xamarin team as they discuss the latest and greatest for Xamarin. This week we sit down with Eilon Lipton to discuss the latest in the Mobile #Blazor Bindings. #microsoft #XamarinForms #Blazor",
                    MediaType = OwlType.FewImages,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/Emuf9aiXEAcjdXw?format=jpg&name=small",
                        "https://pbs.twimg.com/media/Empy-qvWEAIz7CT?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmplPyCW8AAL9g7?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmoZtwnW8AIbaO2?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmfLMz9WEAAinng?format=jpg&name=small",
                        "https://pbs.twimg.com/media/EmeURLUWMAgBJb_?format=jpg&name=small"
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 43,
                    AuthorId = 0,
                    Date = new DateTime(2020, 11, 28, 21, 48, 0),
                    Text = $"#FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse! #democrats #itsucks",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
            };

            _owlsMock[8].LikesList.Insert(0, 0);
            _owlsMock[8].SavesList.Insert(0, 3);

            _owlsMock[6].LikesList.Insert(0, 1);

            _owlsMock[4].LikesList.Insert(0, 3);
            _owlsMock[4].LikesList.Insert(0, 1);
            _owlsMock[4].SavesList.Insert(0, 1);
        }

        #endregion

    }
}
