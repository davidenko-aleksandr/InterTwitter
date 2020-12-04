using InterTwitter.ViewModels.Helpers;
using Prism.Mvvm;
using System;
using Xamarin.Forms;

namespace InterTwitter.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();

            ViewModelLocator.SetAutowireViewModel(this, true);
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
