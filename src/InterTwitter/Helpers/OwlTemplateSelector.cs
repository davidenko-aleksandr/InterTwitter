using InterTwitter.ViewModels.HomePageItems;
using Xamarin.Forms;

namespace InterTwitter.Enums
{
    public class OwlTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OwlOneImageTemplate { get; set; }

        public DataTemplate OwlFewImageTemplate { get; set; }

        public DataTemplate OwlAlbumTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is OwlOneImageViewModel)
            {
                return OwlOneImageTemplate;
            }

            if (item is OwlFewImagesViewModel) 
            {
                return OwlFewImageTemplate;
            }

            if (item is OwlAlbumViewModel)
            {
                return OwlAlbumTemplate;
            }

            return null;
        }
    }
}
