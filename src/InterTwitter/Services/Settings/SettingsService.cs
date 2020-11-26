using Plugin.Settings.Abstractions;

namespace InterTwitter.Services.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _appSettings;

        public SettingsService(ISettings appSettings)
        {
            _appSettings = appSettings;
        }

        #region -- ISettingsService implementation --

        public int AuthorizedUserId
        {
            get => _appSettings.GetValueOrDefault(nameof(AuthorizedUserId), Constants.NoAuthorizedUser);
            set => _appSettings.AddOrUpdateValue(nameof(AuthorizedUserId), value);
        }

        public void ResetSettings()
        {
            AuthorizedUserId = Constants.NoAuthorizedUser;
        }

        #endregion

    }
}
