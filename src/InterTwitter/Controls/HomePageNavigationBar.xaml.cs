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

        public static BindableProperty OpenMenuCommandProperty = BindableProperty.Create(
            propertyName: nameof(OpenMenuCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(AuthorizationNavigationBar));
        public ICommand OpenMenuCommand
        {
            get => (ICommand)GetValue(OpenMenuCommandProperty);
            set => SetValue(OpenMenuCommandProperty, value);
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