using Android.Content;
using InterTwitter.Controls;
using InterTwitter.Droid.Renderers.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace InterTwitter.Droid.Renderers.Controls
{
    public class BorderlessEntryRenderer : EntryRenderer
    {

        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetPadding(0, 0, 0, 0);
                Control.Background = null;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Control is null");
            }

        }

        #endregion

    }
}