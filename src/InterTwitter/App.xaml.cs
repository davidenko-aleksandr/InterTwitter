<<<<<<< HEAD
﻿using InterTwitter.Services.Authorization;
=======
﻿using Acr.UserDialogs;
using InterTwitter.Services.Authorization;
>>>>>>> 786f357b32fe15819f7569d2b6298ba118801c34
using InterTwitter.ViewModels;
using InterTwitter.ViewModels.Authorization;
using InterTwitter.Views;
using InterTwitter.Views.Authorization;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

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

            await NavigationService.NavigateAsync($"{nameof(MenuPage)}");
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

<<<<<<< HEAD
=======
            //packages
            containerRegistry.RegisterInstance(UserDialogs.Instance);

>>>>>>> 786f357b32fe15819f7569d2b6298ba118801c34
            //services
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
        }

        #endregion
    }
}
