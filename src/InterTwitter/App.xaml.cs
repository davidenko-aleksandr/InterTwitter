using Acr.UserDialogs;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.UserService;
using InterTwitter.Services.Settings;
using InterTwitter.ViewModels;
using InterTwitter.Services.Owl;
using InterTwitter.Views;
using Plugin.Settings;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using InterTwitter.Services.PostAction;
using InterTwitter.Services.Notification;
using Plugin.Media;
using InterTwitter.Services.Permission;
using DLToolkit.Forms.Controls;

namespace InterTwitter
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null)
                   : base(initializer)
        {
        }

        #region -- Overrides --

        protected override void OnInitialized()
        {
            InitializeComponent();

            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            FlowListView.Init();

            MainPage = new LogInPage();

            //await NavigationService.NavigateAsync(nameof(LogInPage));
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
            containerRegistry.RegisterForNavigation<BookmarksPage, BookmarksPageViewModel>();
            containerRegistry.RegisterForNavigation<NotificationsPage, NotificationsPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<PostPage, PostPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangeProfilePage, ChangeProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<AddPostPage, AddPostPageViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage>(nameof(MainTabbedPage));

            //plugins
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance(CrossSettings.Current);
            containerRegistry.RegisterInstance(CrossMedia.Current);

            //services
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<ISettingsService>(Container.Resolve<SettingsService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IOwlService>(Container.Resolve<OwlService>());
            containerRegistry.RegisterInstance<INotificationService>(Container.Resolve<NotificationService>());
            containerRegistry.RegisterInstance<IPostActionService>(Container.Resolve<PostActionService>());
            containerRegistry.RegisterInstance<IPermissionService>(Container.Resolve<PermissionService>());
        }

        #endregion

    }
}
