using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Owl;
using InterTwitter.Services.Settings;
using InterTwitter.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterTwitter.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;
        private readonly IOwlService _owlService;

        private List<NotificationModel> _notificationMock;

        public NotificationService(
            ISettingsService settingsService,
            IUserService userService,
            IOwlService owlService,
            IAuthorizationService authorizationService)
        {
            _settingsService = settingsService;
            _authorizationService = authorizationService;
            _owlService = owlService;
            _userService = userService;

            InitMock();
        }

        #region -- INotificationService implementation --

        public async Task<AOResult<bool>> AddNotificationAsync(OwlModel actionOwl, OwlAction action)
        {
            var result = new AOResult<bool>();

            try
            {
                var authorizedUser = await _authorizationService.GetAuthorizedUserAsync();

                if (authorizedUser != null)
                {
                    var user = authorizedUser.Result;
                    var existNotification = _notificationMock.FirstOrDefault(x => x.Owl.Id == actionOwl.Id && x.User.Id == user.Id && x.Action == action);

                    if (existNotification == null && user.Id != actionOwl.Author.Id)
                    {
                        var newNotification = new NotificationModel
                        {
                            Id = _notificationMock.Count,
                            Owl = actionOwl,
                            Author = actionOwl.Author,
                            User = user,
                            Action = action,
                        };

                        _notificationMock.Insert(0, newNotification);
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

        public async Task<AOResult<IEnumerable<NotificationModel>>> GetNotificationCollectionAsync()
        {
            var result = new AOResult<IEnumerable<NotificationModel>>();

            try
            {
                var authorizedUserId = _settingsService.AuthorizedUserId;

                if (_notificationMock != null && authorizedUserId != Constants.NoAuthorizedUser)
                {
                    var currentUserCollection = _notificationMock.Where(x => x.Author.Id == authorizedUserId);

                    result.SetSuccess(currentUserCollection);
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
            var usersResult = await _userService.GetUsersAsync();
            var owlsResult = await _owlService.GetAllOwlsAsync();
            var users = usersResult.Result.ToList();
            var owls = owlsResult.Result.ToList();

            var notifications = new List<NotificationModel>
                {
                    new NotificationModel
                    {
                        Id = 0,
                        Author = users[3],
                        User = users[0],
                        Owl = owls[8],
                        Action = OwlAction.Liked,
                    },
                    new NotificationModel
                    {
                        Id = 1,
                        Author = users[3],
                        User = users[0],
                        Owl = owls[8],
                        Action = OwlAction.Saved,
                    },
                    new NotificationModel
                    {
                        Id = 2,
                        Author = users[3],
                        User = users[1],
                        Owl = owls[6],
                        Action = OwlAction.Liked,
                    },
                    new NotificationModel
                    {
                        Id = 3,
                        Author = users[0],
                        User = users[3],
                        Owl = owls[4],
                        Action = OwlAction.Liked,
                    },
                    new NotificationModel
                    {
                        Id = 4,
                        Author = users[3],
                        User = users[1],
                        Owl = owls[4],
                        Action = OwlAction.Liked,
                    },
                    new NotificationModel
                    {
                        Id = 5,
                        Author = users[3],
                        User = users[1],
                        Owl = owls[4],
                        Action = OwlAction.Saved,
                    },
                    new NotificationModel
                    {
                        Id = 6,
                        Author = users[3],
                        User = users[1],
                        Owl = owls[10],
                        Action = OwlAction.Liked,
                    }
                };

            _notificationMock = notifications;
        }

        #endregion

    }
}
