namespace InterTwitter.Services.Settings
{
    public interface ISettingsService
    {
        int AuthorizedUserId { get; set; }
        void ResetSettings();
    }
}
