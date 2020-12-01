using InterTwitter.ViewModels.Helpers;
using System;
using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {           
            if (BindingContext is IAppearingAware bindable)
            {
                bindable.OnAppearing();
            }
        }
    }
}
