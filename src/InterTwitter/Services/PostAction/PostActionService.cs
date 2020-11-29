using InterTwitter.Enums;
using InterTwitter.Helpers;
using InterTwitter.Models;
using InterTwitter.Services.Notification;
using InterTwitter.Services.Owl;
using InterTwitter.Services.Settings;
using System;
using System.Threading.Tasks;

namespace InterTwitter.Services.PostAction
{
    public class PostActionService : IPostActionService
    {
        private readonly ISettingsService _settingsService;
        private readonly INotificationService _notificationService;
        private readonly IOwlService _owlService;

        public PostActionService(
                                 ISettingsService settingsService,
                                 INotificationService notificationService,
                                 IOwlService owlService)
        {
            _settingsService = settingsService;
            _owlService = owlService;
            _notificationService = notificationService;
        }

        #region -- IPostActionService implementation --

        public async Task<AOResult<bool>> SaveActionAsync(OwlModel actionOwl, OwlAction action)
        {
            var result = new AOResult<bool>();

            try
            {
                var authorizedUserId = _settingsService.AuthorizedUserId;
         
                if (authorizedUserId != Constants.NoAuthorizedUser)
                {
                    switch (action)
                    {
                        case OwlAction.Liked:
                            {
                                var isExist = actionOwl.likesList.Contains(authorizedUserId);

                                if (isExist)
                                {
                                    actionOwl.likesList.Remove(authorizedUserId);
                                }
                                else
                                {
                                    actionOwl.likesList.Insert(0, authorizedUserId);
                                }

                                break;
                            }
                        case OwlAction.Saved:
                            {
                                var isExist = actionOwl.savesList.Contains(authorizedUserId);

                                if (isExist)
                                {
                                    actionOwl.savesList.Remove(authorizedUserId);
                                }
                                else
                                {
                                    actionOwl.savesList.Insert(0, authorizedUserId);
                                }

                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    var addNotifocationResult = await _notificationService.AddNotificationAsync(actionOwl, action);
                    var owlUpdateResult = await _owlService.UpdateOwlAsync(actionOwl);

                    if(addNotifocationResult.Result && owlUpdateResult.Result)
                    {
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
                result.SetError($"{nameof(SaveActionAsync)}: exception", "Something went wrong", ex);
            }

            return result;
        }

        #endregion
    }
}
