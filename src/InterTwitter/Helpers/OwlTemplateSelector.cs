using InterTwitter.ViewModels.HomePageItems;
using System.Diagnostics;
using Xamarin.Forms;

namespace InterTwitter.Enums
{
    public class OwlTemplateSelector : DataTemplateSelector
    {
        #region -- Public properties --

        public DataTemplate OwlOneImageTemplate { get; set; }

        public DataTemplate OwlFewImageTemplate { get; set; }

        public DataTemplate OwlAlbumTemplate { get; set; }

        public DataTemplate OwlNoMediaTemplate { get; set; }

        #endregion

        #region -- Overrides --
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is OwlOneImageViewModel)
            {
                return OwlOneImageTemplate;
            }
            else
            {
                Debug.WriteLine($"{nameof(OwlTemplateSelector)} - {nameof(SelectTemplate)} item is null or can't be executed;");
            }

            if (item is OwlFewImagesViewModel) 
            {
                return OwlFewImageTemplate;
            }
            else
            {
                Debug.WriteLine($"{nameof(OwlTemplateSelector)} - {nameof(SelectTemplate)} item is null or can't be executed;");
            }

            if (item is OwlAlbumViewModel)
            {
                return OwlAlbumTemplate;
            }
            else
            {
                Debug.WriteLine($"{nameof(OwlTemplateSelector)} - {nameof(SelectTemplate)} item is null or can't be executed;");
            }

            if (item is OwlNoMediaViewModel)
            {
                return OwlNoMediaTemplate;
            }
            else
            {
                Debug.WriteLine($"{nameof(OwlTemplateSelector)} - {nameof(SelectTemplate)} item is null or can't be executed;");
            }

            return null;
        }

        #endregion
    }
}
