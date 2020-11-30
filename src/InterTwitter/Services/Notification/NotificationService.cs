using InterTwitter.Enums;
using InterTwitter.Extensions;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using InterTwitter.ViewModels.NotificationItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;

        private Stack<NotificationModel> _notificationMock;

        public NotificationService(
                                   ISettingsService settingsService,
                                   IUserService userService,
                                   IAuthorizationService authorizationService)
        {
            _settingsService = settingsService;
            _authorizationService = authorizationService;

            InitMock();
        }

        #region -- INotificationService implementation --

        public async Task<AOResult<bool>> AddNotificationAsync(OwlModel actionOwl, OwlAction action)
        {
            var result = new AOResult<bool>();

            try
            {
                var authorizedUser = await _authorizationService.GetAuthorizedUserAsync();

                if (authorizedUser is not null)
                {
                    var user = authorizedUser.Result;
                    var existNotification = _notificationMock.FirstOrDefault(x => x.OwlId == actionOwl.Id && x.UserId == user.Id && x.Action == action);

                    if (existNotification is null && user.Id != actionOwl.AuthorId)
                    {
                        var newNotification = new NotificationModel
                        {
                            Id = _notificationMock.Count,
                            AuthorId = actionOwl.AuthorId,
                            UserAvatar = user.Avatar,
                            OwlId = actionOwl.Id,
                            UserId = user.Id,
                            UserName = user.Name,
                            Action = action,
                            OwlText = actionOwl.Text
                        };

                        _notificationMock.Push(newNotification);
                    }
                    else
                    {
                        //existNotification isn't null or user.Id == actionOwl.AuthorId
                    }

                    result.SetSuccess(true);
                }
                else
                {
                    result.SetFailure();
                }
            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(AddNotificationAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        public async Task<AOResult<ObservableCollection<NotificationViewModel>>> GetNotificationCollectionAsync()
        {
            var result = new AOResult<ObservableCollection<NotificationViewModel>>();

            try
            {
                var authorizedUserId = _settingsService.AuthorizedUserId;

                if (_notificationMock is not null && authorizedUserId != Constants.NoAuthorizedUser)
                {
                    var currentUserCollection = _notificationMock.Where(x => x.AuthorId == authorizedUserId);
                    var finalCollection = new ObservableCollection<NotificationViewModel>();

                    foreach (var item in currentUserCollection)
                    {
                        var viewModel = item.ConvertToViewModel();
                        finalCollection.Add(viewModel);
                    }

                    result.SetSuccess(finalCollection);
                }
                else
                {
                    result.SetFailure();
                }

            }
            catch (Exception ex)
            {
                result.SetError($"{nameof(GetNotificationCollectionAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion

        #region -- Private helpers --

        private async Task InitMock()
        {
                var notifications = new List<NotificationModel>
                {
                    new NotificationModel
                    {
                        Id = 0,
                        AuthorId = 3,
                        UserId = 0,
                        OwlId = 8,
                        UserAvatar = "https://pbs.twimg.com/profile_images/874276197357596672/kUuht00m_400x400.jpg",
                        UserName = "Donald J. Trump",
                        Action = OwlAction.Liked,
                        OwlText = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild"
                    },
                    new NotificationModel
                    {
                        Id = 1,
                        AuthorId = 3,
                        UserId = 0,
                        OwlId = 8,
                        UserAvatar = "https://pbs.twimg.com/profile_images/874276197357596672/kUuht00m_400x400.jpg",
                        UserName = "Donald J. Trump",
                        Action = OwlAction.Saved,
                        OwlText = $"Code, collaborate, and ship from anywhere. Get the developer tools and platform to keep remote teams productive. #MSBuild"
                    },
                    new NotificationModel
                    {
                        Id = 2,
                        AuthorId = 3,
                        UserId = 1,
                        OwlId = 6,
                        UserAvatar = "https://pbs.twimg.com/profile_images/1298649731980238848/29o9j4e__400x400.jpg",
                        UserName = "Shakira",
                        Action = OwlAction.Liked,
                        OwlText = $"What?!? fully-functional #XamarinForms sample apps? With source code & walkthroughs? Free? Yes, please! #xamarin #devcommunity #dotnet"
                    },
                    new NotificationModel
                    {
                        Id = 3,
                        AuthorId = 0,
                        UserId = 3,
                        OwlId = 4,
                        UserAvatar = "https://pbs.twimg.com/profile_images/874276197357596672/kUuht00m_400x400.jpg",
                        UserName = "Donald J. Trump",
                        Action = OwlAction.Liked,
                        OwlText = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous"
                    },
                    new NotificationModel
                    {
                        Id = 4,
                        AuthorId = 3,
                        UserId = 1,
                        OwlId = 4,
                        UserAvatar = "https://pbs.twimg.com/profile_images/1298649731980238848/29o9j4e__400x400.jpg",
                        UserName = "Shakira",
                        Action = OwlAction.Liked,
                        OwlText = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous"
                    },
                    new NotificationModel
                    {
                        Id = 5,
                        AuthorId = 3,
                        UserId = 1,
                        OwlId = 4,
                        UserAvatar = "https://pbs.twimg.com/profile_images/1298649731980238848/29o9j4e__400x400.jpg",
                        UserName = "Shakira",
                        Action = OwlAction.Saved,
                        OwlText = $"Go beyond Hello World. In today's guest post, #MSMVP Tim_Lariviere discusses importants part of writing real world apps and how to leverage functional programming with the Model-View-Update architecture to build mobile and desktop apps with #Fabulous"
                    },
                    new NotificationModel
                    {
                        Id = 6,
                        AuthorId = 0,
                        UserId = 3,
                        OwlId = 0,
                        UserAvatar = "https://pbs.twimg.com/profile_images/471641515756769282/RDXWoY7W_400x400.png",
                        UserName = "Xamarin",
                        Action = OwlAction.Liked,
                        OwlText = $"FoxNews daytime is virtually unwatchable, especially during the weekends. Watch OANN, newsmax, or almost anything else.You won’t have to suffer through endless interviews with Democrats, and even worse!"
                    }
                };

            _notificationMock = new Stack<NotificationModel>(notifications);
        }

        #endregion

    }
}
