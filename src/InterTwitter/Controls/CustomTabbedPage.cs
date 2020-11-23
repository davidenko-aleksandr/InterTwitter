using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomTabbedPage : TabbedPage
    {
        public CustomTabbedPage()
        {
        }

        public static readonly BindableProperty SelectedPageNameProperty =
            BindableProperty.Create(
                propertyName: nameof(SelectedPageName),
                returnType: typeof(string),
                declaringType: typeof(CustomTabbedPage),
                defaultBindingMode: BindingMode.TwoWay);

        public string SelectedPageName
        {
            get => (string)GetValue(SelectedPageNameProperty);
            set => SetValue(SelectedPageNameProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CurrentPage) && CurrentPage != null)
            {
                SelectedPageName = CurrentPage.GetType().Name;
            }
        }
    }
}
