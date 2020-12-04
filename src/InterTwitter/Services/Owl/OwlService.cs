using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
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

        public OwlService(
            IUserService userService,
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
                if (owlModel != null)
                {
                    var userResult = await _authorizationService.GetAuthorizedUserAsync();
                    if (userResult.IsSuccess)
                    {
                        var author = userResult.Result;

                        owlModel.Id = _owlsMock.Count;
                        owlModel.Author = author;

                        _owlsMock.Insert(0, owlModel);

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
                result.SetError($"{nameof(AddOwlAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<OwlModel>>> GetAllOwlsAsync(string searchQuery = null)
        {
            var result = new AOResult<IEnumerable<OwlModel>>();

            try
            {
                List<OwlModel> owls = _owlsMock;

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    owls = new List<OwlModel>(owls.Where(x =>
                    x.Author.Name.ToUpper().Contains(searchQuery?.ToUpper()) ||
                    x.Text.ToUpper().Contains(searchQuery?.ToUpper())));
                }
                else
                {
                    //searchQuery is not given
                }

                await Task.Delay(300);

                if (owls != null)
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

        public async Task<AOResult<IEnumerable<OwlModel>>> GetSavedOwlsAsync()
        {
            var result = new AOResult<IEnumerable<OwlModel>>();

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

        public async Task<AOResult<OwlModel>> GetOwlById(int owlId)
        {
            var result = new AOResult<OwlModel>();

            try
            {
                var owlsResult = await GetAllOwlsAsync();
                if (owlsResult.IsSuccess)
                {
                    var owl = owlsResult.Result.FirstOrDefault(x => x.Id == owlId);
                    if (owl != null)
                    {
                        result.SetSuccess(owl);
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
                result.SetError($"{nameof(GetOwlById)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<IEnumerable<OwlModel>>> GetLikedOwlsAsync(int authorId)
        {
            var result = new AOResult<IEnumerable<OwlModel>>();

            try
            {
                var owlsResult = await GetAllOwlsAsync();

                if (owlsResult.IsSuccess)
                {
                    var owls = owlsResult.Result.Where(x => x.LikesList.Contains(authorId));

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

        public async Task<AOResult<IEnumerable<OwlModel>>> GetAuthorOwlsAsync(int authorId)
        {
            var result = new AOResult<IEnumerable<OwlModel>>();

            try
            {
                var owlsResult = await GetAllOwlsAsync();
                if (owlsResult.IsSuccess)
                {
                    var owls = owlsResult.Result.Where(x => x.Author.Id == authorId);

                    result.SetSuccess(owls);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetAuthorOwlsAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<bool>> UpdateOwlAsync(OwlModel owl)
        {
            var result = new AOResult<bool>();

            try
            {
                var changingOwl = _owlsMock.FirstOrDefault(x => x.Id == owl.Id);

                if (changingOwl != null)
                {
                    changingOwl.LikesList = owl.LikesList;
                    changingOwl.SavesList = owl.SavesList;

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
            var usersResult = await _userService.GetUsersAsync();
            var users = usersResult.Result.ToList();

            _owlsMock = new List<OwlModel>()
            {
                new OwlModel
                {
                    Id = 0,
                    Author = users[0],
                    Date = new DateTime(2020, 11, 28, 21, 48, 0),
                    Text = $"FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse!",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 1,
                    Author = users[0],
                    Date = new DateTime(2020, 11, 20, 12, 00, 00),
                    Text = "Look at this in Michigan! A day AFTER the election, Biden receives a dump of 134,886 votes at 6:31AM!",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnOyo7eXcAURaz6?format=jpg&name=large",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 2,
                    Author = users[0],
                    Date = new DateTime(2019, 3, 1, 15, 40, 00),
                    Text = "So true!",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://www.learningcontainer.com/wp-content/uploads/2020/05/sample-mp4-file.mp4",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 3,
                    Author = users[2],
                    Date = DateTime.Now,
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://i.gifer.com/Ar.gif",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 4,
                    Author = users[3],
                    Date = new DateTime(2020, 11, 25, 18, 30, 25),
                    Text = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnsDBAZW4AAov-H?format=jpg&name=large",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 5,
                    Author = users[3],
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
                    Id = 6,
                    Author = users[3],
                    Date = new DateTime(2020, 8, 5, 18, 30, 25),
                    Text = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://uc.uxpin.com/files/614612/609600/www.GIFCreator.me_W6dTbP.gif",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 7,
                    Author = users[3],
                    Date = new DateTime(2020, 7, 2, 18, 30, 25),
                    Text = $"This guest post by Charlin Agramonte elaborates on how multilingual support is one of the most common requirements for mobile apps and the simplicity of building mobile apps with #Xamarin that handle multiple languages.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 8,
                    Author = users[3],
                    Date = new DateTime(2020, 3, 4, 18, 30, 25),
                    Text = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "http://vjs.zencdn.net/v/oceans.mp4",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 9,
                    Author = users[2],
                    Date = new DateTime(2019, 11, 6, 18, 30, 25),
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 10,
                    Author = users[3],
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
                        "https://pbs.twimg.com/media/EmeURLUWMAgBJb_?format=jpg&name=small",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 11,
                    Author = users[0],
                    Date = new DateTime(2020, 11, 20, 12, 00, 00),
                    Text = "Look at this in Michigan! A day AFTER the election, Biden receives a dump of 134,886 votes at 6:31AM!",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnOyo7eXcAURaz6?format=jpg&name=large",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 12,
                    Author = users[0],
                    Date = new DateTime(2019, 3, 1, 15, 40, 00),
                    Text = "So true!",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1329535287735816194",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 13,
                    Author = users[2],
                    Date = DateTime.Now,
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 14,
                    Author = users[3],
                    Date = new DateTime(2020, 11, 25, 18, 30, 25),
                    Text = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnsDBAZW4AAov-H?format=jpg&name=large",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 15,
                    Author = users[3],
                    Date = new DateTime(2020, 8, 14, 18, 30, 25),
                    Text = $"Measure, optimize, and fine-tune the #performance of your #Android apps with #Xamarin & #XamarinForms with these tips and tricks by JonathanPeppers",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EfZTe2JWoAAsUuE?format=png&name=small",
                        "https://pbs.twimg.com/media/Ee1a-gVXgAEZcFT?format=png&name=small",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 16,
                    Author = users[3],
                    Date = new DateTime(2020, 8, 5, 18, 30, 25),
                    Text = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 17,
                    Author = users[3],
                    Date = new DateTime(2020, 7, 2, 18, 30, 25),
                    Text = $"This guest post by Charlin Agramonte elaborates on how multilingual support is one of the most common requirements for mobile apps and the simplicity of building mobile apps with #Xamarin that handle multiple languages.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 18,
                    Author = users[3],
                    Date = new DateTime(2020, 3, 4, 18, 30, 25),
                    Text = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1262782634335383554",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 19,
                    Author = users[2],
                    Date = new DateTime(2019, 11, 6, 18, 30, 25),
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 20,
                    Author = users[3],
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
                        "https://pbs.twimg.com/media/EmeURLUWMAgBJb_?format=jpg&name=small",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 21,
                    Author = users[0],
                    Date = new DateTime(2020, 11, 28, 21, 48, 0),
                    Text = $"FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse!",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 22,
                    Author = users[0],
                    Date = new DateTime(2020, 11, 20, 12, 00, 00),
                    Text = "Look at this in Michigan! A day AFTER the election, Biden receives a dump of 134,886 votes at 6:31AM!",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnOyo7eXcAURaz6?format=jpg&name=large",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 23,
                    Author = users[0],
                    Date = new DateTime(2019, 3, 1, 15, 40, 00),
                    Text = "So true!",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1329535287735816194",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 24,
                    Author = users[2],
                    Date = DateTime.Now,
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 25,
                    Author = users[3],
                    Date = new DateTime(2020, 11, 25, 18, 30, 25),
                    Text = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnsDBAZW4AAov-H?format=jpg&name=large",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 26,
                    Author = users[3],
                    Date = new DateTime(2020, 8, 14, 18, 30, 25),
                    Text = $"Measure, optimize, and fine-tune the #performance of your #Android apps with #Xamarin & #XamarinForms with these tips and tricks by JonathanPeppers",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EfZTe2JWoAAsUuE?format=png&name=small",
                        "https://pbs.twimg.com/media/Ee1a-gVXgAEZcFT?format=png&name=small",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 27,
                    Author = users[3],
                    Date = new DateTime(2020, 8, 5, 18, 30, 25),
                    Text = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 28,
                    Author = users[3],
                    Date = new DateTime(2020, 7, 2, 18, 30, 25),
                    Text = $"This guest post by Charlin Agramonte elaborates on how multilingual support is one of the most common requirements for mobile apps and the simplicity of building mobile apps with #Xamarin that handle multiple languages.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 29,
                    Author = users[3],
                    Date = new DateTime(2020, 3, 4, 18, 30, 25),
                    Text = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1262782634335383554",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 30,
                    Author = users[2],
                    Date = new DateTime(2019, 11, 6, 18, 30, 25),
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 31,
                    Author = users[3],
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
                        "https://pbs.twimg.com/media/EmeURLUWMAgBJb_?format=jpg&name=small",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 32,
                    Author = users[0],
                    Date = new DateTime(2020, 11, 28, 21, 48, 0),
                    Text = $"FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse!",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 33,
                    Author = users[0],
                    Date = new DateTime(2020, 11, 20, 12, 00, 00),
                    Text = "Look at this in Michigan! A day AFTER the election, Biden receives a dump of 134,886 votes at 6:31AM!",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnOyo7eXcAURaz6?format=jpg&name=large",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 34,
                    Author = users[0],
                    Date = new DateTime(2019, 3, 1, 15, 40, 00),
                    Text = "So true!",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1329535287735816194",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 35,
                    Author = users[2],
                    Date = DateTime.Now,
                    Text = "Descriptions - this is more text jrtv rt rt br br brbref fewfe fege veerv e",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 36,
                    Author = users[3],
                    Date = new DateTime(2020, 11, 25, 18, 30, 25),
                    Text = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EnsDBAZW4AAov-H?format=jpg&name=large",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 37,
                    Author = users[3],
                    Date = new DateTime(2020, 8, 14, 18, 30, 25),
                    Text = $"Measure, optimize, and fine-tune the #performance of your #Android apps with #Xamarin & #XamarinForms with these tips and tricks by JonathanPeppers",
                    MediaType = OwlType.OneImage,
                    Media = new List<string>()
                    {
                        "https://pbs.twimg.com/media/EfZTe2JWoAAsUuE?format=png&name=small",
                        "https://pbs.twimg.com/media/Ee1a-gVXgAEZcFT?format=png&name=small",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 38,
                    Author = users[3],
                    Date = new DateTime(2020, 8, 5, 18, 30, 25),
                    Text = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 39,
                    Author = users[3],
                    Date = new DateTime(2020, 7, 2, 18, 30, 25),
                    Text = $"This guest post by Charlin Agramonte elaborates on how multilingual support is one of the most common requirements for mobile apps and the simplicity of building mobile apps with #Xamarin that handle multiple languages.",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 40,
                    Author = users[3],
                    Date = new DateTime(2020, 3, 4, 18, 30, 25),
                    Text = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild #microsoft",
                    MediaType = OwlType.Video,
                    Media = new List<string>()
                    {
                        "https://twitter.com/i/status/1262782634335383554",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 41,
                    Author = users[2],
                    Date = new DateTime(2019, 11, 6, 18, 30, 25),
                    Text = "Rocky Balboa is a 2006 American sports drama film written, directed by, and starring Sylvester Stallone. #StalloneRules #Number1 #sports",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 42,
                    Author = users[3],
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
                        "https://pbs.twimg.com/media/EmeURLUWMAgBJb_?format=jpg&name=small",
                    },
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
                new OwlModel
                {
                    Id = 43,
                    Author = users[0],
                    Date = new DateTime(2020, 11, 28, 21, 48, 0),
                    Text = $"#FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse! #democrats #itsucks",
                    MediaType = OwlType.NoMedia,
                    LikesList = new List<int>(),
                    SavesList = new List<int>(),
                },
            };

            _owlsMock[8].LikesList.Insert(0, 0);
            _owlsMock[8].LikesList.Insert(0, 3);
            _owlsMock[8].LikesList.Insert(0, 2);
            _owlsMock[8].LikesList.Insert(0, 1);
            _owlsMock[8].SavesList.Insert(0, 3);

            _owlsMock[1].LikesList.Insert(0, 0);
            _owlsMock[1].LikesList.Insert(0, 3);
            _owlsMock[1].LikesList.Insert(0, 2);
            _owlsMock[1].LikesList.Insert(0, 1);

            _owlsMock[2].LikesList.Insert(0, 3);
            _owlsMock[2].LikesList.Insert(0, 2);
            _owlsMock[2].LikesList.Insert(0, 1);
            _owlsMock[2].LikesList.Insert(0, 0);

            _owlsMock[12].LikesList.Insert(0, 0);
            _owlsMock[12].LikesList.Insert(0, 3);
            _owlsMock[12].LikesList.Insert(0, 2);
            _owlsMock[12].LikesList.Insert(0, 1);
            _owlsMock[12].SavesList.Insert(0, 1);

            _owlsMock[23].LikesList.Insert(0, 0);
            _owlsMock[23].LikesList.Insert(0, 3);
            _owlsMock[23].LikesList.Insert(0, 2);
            _owlsMock[23].LikesList.Insert(0, 1);
            _owlsMock[23].SavesList.Insert(0, 3);

            _owlsMock[33].LikesList.Insert(0, 0);
            _owlsMock[33].LikesList.Insert(0, 3);
            _owlsMock[33].LikesList.Insert(0, 2);
            _owlsMock[33].LikesList.Insert(0, 1);
            _owlsMock[33].SavesList.Insert(0, 3);

            _owlsMock[6].LikesList.Insert(0, 0);
            _owlsMock[6].LikesList.Insert(0, 3);
            _owlsMock[6].LikesList.Insert(0, 2);
            _owlsMock[6].LikesList.Insert(0, 1);

            _owlsMock[43].LikesList.Insert(0, 2);
            _owlsMock[23].LikesList.Insert(0, 2);

            _owlsMock[6].SavesList.Insert(0, 3);

            _owlsMock[11].LikesList.Insert(0, 0);
            _owlsMock[11].LikesList.Insert(0, 1);
            _owlsMock[4].LikesList.Insert(0, 3);
            _owlsMock[4].LikesList.Insert(0, 1);
            _owlsMock[4].SavesList.Insert(0, 1);
        }

        #endregion

    }
}
