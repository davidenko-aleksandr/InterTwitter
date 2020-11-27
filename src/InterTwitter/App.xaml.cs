using Acr.UserDialogs;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.UserService;
using InterTwitter.Services.Settings;
using InterTwitter.ViewModels;
using InterTwitter.Views;
using Plugin.Settings;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Media;

namespace InterTwitter
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null)
            : base(initializer)
        {
        }

        #region -- Overrides --

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigateAsync();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //pages & viewmodels
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpMainPage, SignUpMainPageViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPasswordPage, SignUpPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<MessagesPage, MessagesPageViewModel>();
            containerRegistry.RegisterForNavigation<NotificationsPage, NotificationsPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<ChangeProfilePage,ChangeProfilePageViewModel>();

            //plugins
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance(CrossSettings.Current);
            containerRegistry.RegisterInstance(CrossMedia.Current);

            //services
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<ISettingsService>(Container.Resolve<SettingsService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>()); 
        }

        #endregion

        #region -- Private Helpers --

        private async Task NavigateAsync()
        {
            var isAuthorized = Container.Resolve<IAuthorizationService>().IsAuthorized;

            var path = isAuthorized ? nameof(MenuPage) : nameof(LogInPage);

            await NavigationService.NavigateAsync(nameof(path));

            //await NavigationService.NavigateAsync(nameof(ProfilePage));
        }

        #endregion
    }
}
