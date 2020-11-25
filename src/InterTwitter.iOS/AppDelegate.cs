using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using InterTwitter.iOS.Services.Keyboard;
using InterTwitter.Services.Keyboard;
using Prism;
using Prism.Ioc;
using UIKit;

namespace InterTwitter.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        #region -- Overrides --

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        #endregion
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IKeyboardService, KeyboardService>();
        }
    }
}
