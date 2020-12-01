using InterTwitter.ViewModels.HomePageItems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Helpers
{
    public class PostTemplateSelector : DataTemplateSelector
    {
        #region -- Public properties --

        public DataTemplate PostOneImageTemplate { get; set; }

        public DataTemplate PostFewImageTemplate { get; set; }

        public DataTemplate PostNoMediaTemplate { get; set; }

        public DataTemplate PostGifTemplate { get; set; }

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
            else
            {
                //
            }

            return dataTemplate;
        }

        #endregion
    }
}
