using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Helpers
{
   public class ProfilePageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PostsViewTemplate { get; set; }

        public DataTemplate LikesViewTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            return LikesViewTemplate;
        }
    }
}
