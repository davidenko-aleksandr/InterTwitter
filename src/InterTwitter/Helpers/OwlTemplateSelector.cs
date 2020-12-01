﻿using InterTwitter.ViewModels.HomePageItems;
using InterTwitter.ViewModels.OwlItems;
using Xamarin.Forms;

namespace InterTwitter.Enums
{
    public class OwlTemplateSelector : DataTemplateSelector
    {
        #region -- Public properties --

        public DataTemplate OwlOneImageTemplate { get; set; }

        public DataTemplate OwlFewImageTemplate { get; set; }

        public DataTemplate OwlNoMediaTemplate { get; set; }

        public DataTemplate OwlVideoTemplate { get; set; }

        #endregion

        #region -- Overrides --

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            DataTemplate dataTemplate = null;

            if (item is OwlOneImageViewModel)
            {
                dataTemplate = OwlOneImageTemplate;
            }
            else if (item is OwlFewImagesViewModel)
            {
                dataTemplate = OwlFewImageTemplate;
            }
            else if (item is OwlNoMediaViewModel)
            {
                dataTemplate = OwlNoMediaTemplate;
            }
            else if (item is OwlVideoViewModel)
            {
                dataTemplate = OwlVideoTemplate;
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
