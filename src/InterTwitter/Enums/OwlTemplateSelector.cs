using InterTwitter.ViewModels.HomePageItems;
using Xamarin.Forms;

namespace InterTwitter.Enums
{
    public class OwlTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OwlPictureTemplate { get; set; }
        public DataTemplate OwlTwoPicturesTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is OwlPictureViewModel)
                return OwlPictureTemplate;

            if (item is OwlAlbumViewModel)
                return OwlTwoPicturesTemplate;

            return null;
        }
    }
}
