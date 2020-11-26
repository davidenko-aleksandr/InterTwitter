using InterTwitter.Controls;
using InterTwitter.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomCollectionView), typeof(CustomListRenderer))]
namespace InterTwitter.Droid.Renderers
{
    public class CustomListRenderer : ListViewRenderer
    {
        #region -- Overrides --

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            //Control.ScrollStateChanged;
            //e.NewElement.Scrolled

        }

        #endregion
    }
}