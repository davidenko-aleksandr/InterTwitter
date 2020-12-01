using InterTwitter.ViewModels.OwlItems;
using Xamarin.Forms;

namespace InterTwitter.Helpers
{
    public class PostTemplateSelector : DataTemplateSelector
    {
        #region -- Public properties --

        public DataTemplate PostOneImageTemplate { get; set; }

        public DataTemplate PostFewImageTemplate { get; set; }

        public DataTemplate PostNoMediaTemplate { get; set; }

        public DataTemplate PostVideoTemplate { get; set; }

        #endregion

        #region -- Overrides --

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate dataTemplate = null;

            if (item is OwlOneImageViewModel)
            {
                dataTemplate = PostOneImageTemplate;
            }
            else if (item is OwlFewImagesViewModel)
            {
                dataTemplate = PostFewImageTemplate;
            }
            else if (item is OwlNoMediaViewModel)
            {
                dataTemplate = PostNoMediaTemplate;
            }
            else if (item is OwlVideoViewModel)
            {
                dataTemplate = PostVideoTemplate;
            }
            else
            {
                // 
            }

            return dataTemplate;
        }

        #endregion
    }
}
