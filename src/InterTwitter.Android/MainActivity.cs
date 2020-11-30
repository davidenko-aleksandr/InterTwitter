using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Forms;
using FFImageLoading;
using FFImageLoading.Forms.Platform;
using Acr.UserDialogs;
using InterTwitter.Services.Keyboard;
using InterTwitter.Droid.Services.Keyboard;
<<<<<<< HEAD
using PanCardView.Droid;
=======
using Octane.Xamarin.Forms.VideoPlayer.Android;
>>>>>>> 7ad0afba4bc257a379d7ceae909f6605d5b1a489

namespace InterTwitter.Droid
{
    [Activity(Label = "InterTwitter",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region -- Overrides --

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(savedInstanceState);

            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
            };
<<<<<<< HEAD
            ImageService.Instance.Initialize(config);
           
=======
            ImageService.Instance.Initialize(config);            

>>>>>>> 7ad0afba4bc257a379d7ceae909f6605d5b1a489
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            UserDialogs.Init(this);
<<<<<<< HEAD

            CardsViewRenderer.Preserve();


=======
            FormsVideoPlayer.Init();
>>>>>>> 7ad0afba4bc257a379d7ceae909f6605d5b1a489
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IKeyboardService, KeyboardService>();
        }
    }
}