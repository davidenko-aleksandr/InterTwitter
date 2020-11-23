using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Services.Settings
{
    public interface ISettingsService
    {
        string UserEmail { get; set; }
        void ClearData();
    }
}
