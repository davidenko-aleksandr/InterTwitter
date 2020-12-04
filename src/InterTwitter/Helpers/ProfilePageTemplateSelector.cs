using InterTwitter.ViewModels.ProfilePageItems;
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

<<<<<<< HEAD
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) =>
           item switch
           {
               LikesViewModel => LikesViewTemplate,
               PostsViewModel => PostsViewTemplate,
               _ => throw new ArgumentException($"Undefined item in: {nameof(ProfilePageTemplateSelector)}.{nameof(OnSelectTemplate)}")
           };
=======
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return LikesViewTemplate;
        }
>>>>>>> f3bc9c260b87115cba81921ffdd9f5c2b2600fbc
    }
}
