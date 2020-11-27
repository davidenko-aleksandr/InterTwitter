using InterTwitter.Controls;
using InterTwitter.iOS.Renderers.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCollectionView), typeof(CustomCollectionViewRenderer))]
namespace InterTwitter.iOS.Renderers.Controls
{
    public class CustomCollectionViewRenderer : CollectionViewRenderer
    {
        public CustomCollectionViewRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<GroupableItemsView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Controller.CollectionView.Bounces = false;
            }
            else
            {
                //NewElement is null
            }

        }
    }
}