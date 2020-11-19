using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class AuthorizationNavigationBar : Grid
    {
        public AuthorizationNavigationBar()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static BindableProperty IsBackButtonVisibleProperty = BindableProperty.Create(
                                                                                        propertyName: nameof(IsBackButtonVisible),
                                                                                        returnType: typeof(bool),
                                                                                        declaringType: typeof(AuthorizationNavigationBar),
                                                                                        defaultValue: false);
        public bool IsBackButtonVisible
        {
            get => (bool)GetValue(IsBackButtonVisibleProperty);
            set => SetValue(IsBackButtonVisibleProperty, value);
        }

        public static BindableProperty GoBackCommandProperty = BindableProperty.Create(
                                                                                        propertyName: nameof(GoBackCommand),
                                                                                        returnType: typeof(ICommand),
                                                                                        declaringType: typeof(AuthorizationNavigationBar));
        public ICommand GoBackCommand
        {
            get => (ICommand)GetValue(GoBackCommandProperty);
            set => SetValue(GoBackCommandProperty, value);
        }

        #endregion
    }
}