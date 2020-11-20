using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class HomePageNavigationBar : Grid
    {
        public HomePageNavigationBar()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static BindableProperty GoBackCommandProperty = BindableProperty.Create(
            propertyName: nameof(GoBackCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(AuthorizationNavigationBar));
        public ICommand GoBackCommand
        {
            get => (ICommand)GetValue(GoBackCommandProperty);
            set => SetValue(GoBackCommandProperty, value);
        }

        public static BindableProperty EditCommandProperty = BindableProperty.Create(
            propertyName: nameof(EditCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(AuthorizationNavigationBar));
        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        #endregion
    }
}