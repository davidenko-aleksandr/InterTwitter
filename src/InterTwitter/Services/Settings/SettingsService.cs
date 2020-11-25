using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

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

        public string UserEmail
        {
            get => _appSettings.GetValueOrDefault(nameof(UserEmail), string.Empty);
            set => _appSettings.AddOrUpdateValue(nameof(UserEmail), value);
        }

        public void ClearData()
        {
            UserEmail = string.Empty;
        }

        #endregion

    }
}
