using InterTwitter.Resources;
using System;
using System.Diagnostics;
using System.Windows.Input;
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
            Entry = entry;
        }

        public static event EventHandler<TextChangedEventArgs> TextChanged;

        #region -- Public Properties --

        public Entry Entry { get; set; }

        private static readonly BindableProperty IsValidProperty = BindableProperty.Create(
                                                        
                                                        propertyName: nameof(IsValid),
                                                        returnType: typeof(bool),
                                                        declaringType: typeof(CustomEntry),
                                                        defaultValue: false,
                                                        defaultBindingMode: BindingMode.TwoWay);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

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
                                                         defaultValue: string.Empty,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: (bindable, oldValue, newValue) => ((CustomEntry)bindable).OnTextChanged((string)oldValue, (string)newValue));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private ICommand _clearClickCommand;
        public ICommand ClearClickCommand => _clearClickCommand ??= new Command(OnClearClickCommand);

        private ICommand _eyeClickCommand;
        public ICommand EyeClickCommand => _eyeClickCommand ??= new Command(OnEyeClickCommand);

        #endregion

        #region -- Private Helpers --

        private void OnClearClickCommand()
        {
            Text = null;
        }

        private void OnEyeClickCommand()
        {
            if (IsPasswordLocal)
            {
                Eye.Source = AppResource.EyeOnImage;
            }
            else
            {
                Eye.Source = AppResource.EyeOffImage;
            }

            IsPasswordLocal = !IsPasswordLocal;
        }

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

        #endregion

    }
}