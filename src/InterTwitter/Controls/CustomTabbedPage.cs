using System;
using System.Linq;
using System.Runtime.CompilerServices;
using InterTwitter.Views;
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
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnSelectedPageNamePropertyChanged);

        private static void OnSelectedPageNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var newPage = newValue as string;
            var tabbedPage = bindable as TabbedPage;
            
            if (newValue != oldValue && tabbedPage != null && newPage != null)
            {
                switch (newPage)
                {
                    case nameof(HomePage):
                        tabbedPage.CurrentPage = tabbedPage.Children.First(x => x.GetType() == typeof(HomePage));
                        break;
                    case nameof(SearchPage):
                        tabbedPage.CurrentPage = tabbedPage.Children.First(x => x.GetType() == typeof(SearchPage));
                        break;
                    case nameof(NotificationsPage):
                        tabbedPage.CurrentPage = tabbedPage.Children.First(x => x.GetType() == typeof(NotificationsPage));
                        break;
                    case nameof(MessagesPage):
                        tabbedPage.CurrentPage = tabbedPage.Children.First(x => x.GetType() == typeof(MessagesPage));
                        break;
                    default:
                        break;
                }
            }
        }

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
