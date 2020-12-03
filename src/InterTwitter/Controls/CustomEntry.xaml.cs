using InterTwitter.Resources;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class CustomEntry : ContentView
    {
        public CustomEntry()
        {
            InitializeComponent();

            Entry = entry;
            Entry.Focused += OnEntryFocusChanged;
            Entry.Unfocused += OnEntryFocusChanged;
        }

        public static event EventHandler<TextChangedEventArgs> TextChanged;

        #region -- Public Properties --

        public BorderlessEntry Entry { get; set; }

        private static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(CustomEntry), false, BindingMode.OneWay);
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        private static readonly BindableProperty IsEntryFocusedProperty =
            BindableProperty.Create(nameof(IsEntryFocused), typeof(bool), typeof(CustomEntry), false, BindingMode.TwoWay);
        public bool IsEntryFocused
        {
            get => (bool)GetValue(IsEntryFocusedProperty);
            set => SetValue(IsEntryFocusedProperty, value);
        }

        private static readonly BindableProperty IsPasswordLocalProperty =
            BindableProperty.Create(nameof(IsPasswordLocal), typeof(bool), typeof(CustomEntry), false, BindingMode.TwoWay);
        public bool IsPasswordLocal
        {
            get => (bool)GetValue(IsPasswordLocalProperty);
            set => SetValue(IsPasswordLocalProperty, value);
        }

        public static readonly BindableProperty NameTextProperty =
            BindableProperty.Create(nameof(NameText), typeof(string), typeof(CustomEntry), string.Empty, BindingMode.TwoWay);
        public string NameText
        {
            get => (string)GetValue(NameTextProperty);
            set => SetValue(NameTextProperty, value);
        }

        public static readonly BindableProperty ErrorTextProperty =
            BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(CustomEntry), string.Empty, BindingMode.TwoWay);
        public string ErrorText
        {
            get => (string)GetValue(ErrorTextProperty);
            set => SetValue(ErrorTextProperty, value);
        }

        public static readonly BindableProperty IsErrorVisibleProperty =
            BindableProperty.Create(nameof(IsErrorVisible), typeof(bool), typeof(CustomEntry), false, BindingMode.TwoWay);
        public bool IsErrorVisible
        {
            get => (bool)GetValue(IsErrorVisibleProperty);
            set => SetValue(IsErrorVisibleProperty, value);
        }

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(CustomEntry), Color.FromHex("#DEDFE1"), BindingMode.TwoWay);
        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomEntry), false, BindingMode.TwoWay, propertyChanged: OnIsPasswordPropertyChanged);
        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsClearButtonVisibleProperty =
            BindableProperty.Create(nameof(IsClearButtonVisible), typeof(bool), typeof(CustomEntry), false, BindingMode.TwoWay);
        public bool IsClearButtonVisible
        {
            get => (bool)GetValue(IsClearButtonVisibleProperty);
            set => SetValue(IsClearButtonVisibleProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEntry), null, BindingMode.TwoWay);
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(CustomEntry), null, BindingMode.TwoWay);
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public static readonly BindableProperty ReturnTypeProperty =
            BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(CustomEntry), null, BindingMode.TwoWay);
        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntry), string.Empty, BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) => ((CustomEntry)bindable).OnTextChanged((string)oldValue, (string)newValue));
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

        private static void OnIsPasswordPropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var entry = bindable as CustomEntry;

            if (entry != null)
            {
                entry.IsPasswordLocal = (bool)newValue;
            }
            else
            {
                //entry is null
            }
        }

        private void OnClearClickCommand()
        {
            Text = null;
        }

        private void OnEyeClickCommand()
        {
            Eye.Source = IsPasswordLocal ? AppResource.EyeOnImage : AppResource.EyeOffImage;

            IsPasswordLocal = !IsPasswordLocal;
        }

        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                ClearButton.IsVisible = false;
                NameLabel.IsVisible = false;
                EyeButton.IsVisible = false;
            }
            else
            {
                NameLabel.IsVisible = true;

                if (IsPassword)
                {
                    EyeButton.IsVisible = true;
                }
                else
                {
                    //IsPassword is false
                }

                if (IsClearButtonVisible)
                {
                    ClearButton.IsVisible = true;
                }
                else
                {
                    //IsClearButtonVisible is false
                }
            }

            TextChanged?.Invoke(this, new TextChangedEventArgs(oldValue, newValue));
        }

        private void OnEntryFocusChanged(object sender, FocusEventArgs e)
        {
            if (e != null)
            {
                IsEntryFocused = e.IsFocused;
            }
            else
            {
                // e is null
            }
        }

        #endregion

    }
}