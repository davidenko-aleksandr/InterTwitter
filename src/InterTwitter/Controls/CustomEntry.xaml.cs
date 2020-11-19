using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InterTwitter.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntry : ContentView
    {
        public CustomEntry()
        {
            InitializeComponent();
        }

        public static event EventHandler<TextChangedEventArgs> TextChanged;

        #region -- Public Properties --

        private static readonly BindableProperty IsPasswordLocalProperty = BindableProperty.Create(
                                                         propertyName: nameof(IsPasswordLocal),
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: false,
                                                         defaultBindingMode: BindingMode.TwoWay);
        public bool IsPasswordLocal
        {
            get => (bool)GetValue(IsPasswordLocalProperty);
            set => SetValue(IsPasswordLocalProperty, value);
        }

        public static readonly BindableProperty NameTextProperty = BindableProperty.Create(
                                                          propertyName: nameof(NameText),
                                                          returnType: typeof(string),
                                                          declaringType: typeof(CustomEntry),
                                                          defaultValue: string.Empty,
                                                          defaultBindingMode: BindingMode.TwoWay);
        public string NameText
        {
            get => (string)GetValue(NameTextProperty);
            set => SetValue(NameTextProperty, value);
        }

        public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
                                                          propertyName: nameof(ErrorText),
                                                          returnType: typeof(string),
                                                          declaringType: typeof(CustomEntry),
                                                          defaultValue: string.Empty,
                                                          defaultBindingMode: BindingMode.TwoWay);
        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        public static readonly BindableProperty IsErrorVisibleProperty = BindableProperty.Create(
                                                         propertyName: nameof(IsErrorVisible),
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: false,
                                                         defaultBindingMode: BindingMode.TwoWay);
        public bool IsErrorVisible
        {
            get => (bool)GetValue(IsErrorVisibleProperty);
            set => SetValue(IsErrorVisibleProperty, value);
        }

        public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
                                                propertyName: nameof(LineColor),
                                                returnType: typeof(Color),
                                                declaringType: typeof(CustomEntry),
                                                defaultValue: Color.FromHex("#DEDFE1"),
                                                defaultBindingMode: BindingMode.TwoWay);
        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
                                                         propertyName: nameof(IsPassword),
                                                         returnType: typeof(bool),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: false,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: OnIsPasswordPropertyChanged);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
                                                         propertyName: nameof(Placeholder),
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay);
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                                         propertyName: nameof(Text),
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomEntry),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: (bindable, oldValue, newValue) => ((CustomEntry)bindable).OnTextChanged((string)oldValue, (string)newValue));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region -- Private Helpers --

        private static void OnIsPasswordPropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var entry = bindable as CustomEntry;

            if (entry != null)
            {
                entry.IsPasswordLocal = (bool)newValue;
            }
            else
            {
                Debug.WriteLine("entry is null");
            }
        }

        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                ClearButton.IsVisible = false;
                NameLabel.IsVisible = false;
                EyeButton.IsVisible = false;

                if (IsPassword)
                {
                    EyeButton.IsVisible = false;
                }
                else
                {
                    Debug.WriteLine("IsPassword is false");
                }
            }
            else
            {
                ClearButton.IsVisible = true;
                NameLabel.IsVisible = true;
                if (IsPassword)
                {
                    EyeButton.IsVisible = true;
                }
                else
                {
                    Debug.WriteLine("IsPassword is false");
                }
            }

            TextChanged?.Invoke(this, new TextChangedEventArgs(oldValue, newValue));
        }

        private void ClearClick(object sender, EventArgs args)
        {
            Text = null;
        }

        private void EyeClick(object sender, EventArgs args)
        {
            if (IsPasswordLocal)
            {
                (sender as ImageButton).Source = "ic_eye_on.png";
            }
            else
            {
                (sender as ImageButton).Source = "ic_eye_off.png";
            }

            IsPasswordLocal = !IsPasswordLocal;
        }

        #endregion

    }
}