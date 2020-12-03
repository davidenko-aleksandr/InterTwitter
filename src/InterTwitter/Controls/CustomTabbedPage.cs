using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomTabbedPage : TabbedPage
    {
        public CustomTabbedPage()
        {
        }

        #region -- Public properties --

        public static readonly BindableProperty SelectedTabTypeProperty = BindableProperty.Create(
            propertyName: nameof(SelectedTabType),
            returnType: typeof(Type),
            declaringType: typeof(CustomTabbedPage),
            defaultBindingMode: BindingMode.TwoWay);

        public Type SelectedTabType
        {
            get => (Type)GetValue(SelectedTabTypeProperty);
            set => SetValue(SelectedTabTypeProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CurrentPage) && CurrentPage != null)
            {
                SelectedTabType = CurrentPage.GetType();
            }
            else
            {
                //Debug.WriteLine("propertyName isn't CurrentPage and CurrentPage is null");
            }
        }

        #endregion

    }
}
