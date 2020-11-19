using Prism.Mvvm;
using System;
using Xamarin.Forms;

namespace InterTwitter.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModelLocator.SetAutowireViewModel(this, true);
        }
    }
}
