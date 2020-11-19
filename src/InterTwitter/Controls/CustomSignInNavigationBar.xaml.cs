using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class CustomSignInNavigationBar : ContentView
    {
        #region -- Public properties --

        public CustomSignInNavigationBar()
        {
            InitializeComponent();
        }

        public static BindableProperty IsShowBackButtonProperty = BindableProperty.Create(
                                                                                        propertyName: nameof(IsShowBackButton),
                                                                                        returnType: typeof(bool),
                                                                                        declaringType: typeof(CustomSignInNavigationBar),
                                                                                        defaultValue: false);
        public bool IsShowBackButton
        {
            get => (bool)GetValue(IsShowBackButtonProperty);
            set => SetValue(IsShowBackButtonProperty, value);
        }

        public static BindableProperty GoBackCommandProperty = BindableProperty.Create(
                                                                                        propertyName: nameof(GoBackCommand),
                                                                                        returnType: typeof(ICommand),
                                                                                        declaringType: typeof(CustomSignInNavigationBar),
                                                                                        defaultBindingMode: BindingMode.TwoWay,
                                                                                        defaultValue: null);
        public ICommand GoBackCommand
        {
            get => (ICommand)GetValue(GoBackCommandProperty);
            set => SetValue(GoBackCommandProperty, value);
        }

        #endregion
    }
}