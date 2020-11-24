﻿using Acr.UserDialogs;
using InterTwitter.Services.Authorization;
using InterTwitter.Services.Settings;
using InterTwitter.ViewModels;
using InterTwitter.Views;
using Plugin.Settings;
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

            var isAuthorized = Container.Resolve<IAuthorizationService>().IsAuthorized;

            if (isAuthorized)
            {
                await NavigationService.NavigateAsync($"/{nameof(MenuPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync(nameof(LogInPage));
            }
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

            //packages
            containerRegistry.RegisterInstance(UserDialogs.Instance);
            containerRegistry.RegisterInstance(CrossSettings.Current);

            //services
            containerRegistry.RegisterInstance<ISettingsService>(Container.Resolve<SettingsService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
        }

        #endregion
    }
}
