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

        public static readonly BindableProperty SelectedTabTypeProperty =
            BindableProperty.Create(
                propertyName: nameof(SelectedTabType),
                returnType: typeof(Type),
                declaringType: typeof(CustomTabbedPage),
                defaultBindingMode: BindingMode.TwoWay);

        public Type SelectedTabType
        {
            get => (Type)GetValue(SelectedTabTypeProperty);
            set => SetValue(SelectedTabTypeProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CurrentPage) && CurrentPage != null)
            {
                SelectedTabType = CurrentPage.GetType();
            }
        }
    }
}
