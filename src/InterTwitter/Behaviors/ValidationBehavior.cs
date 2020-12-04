using InterTwitter.Controls;
using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using InterTwitter.Validators;

namespace InterTwitter.Behaviors
{
    public class ValidationBehavior : BehaviorBase<CustomEntry>
    {
        private CustomEntry _control;

        #region -- Public Properties --

        public static readonly BindableProperty RegexProperty = BindableProperty.Create(
            propertyName: nameof(Regex),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string Regex
        {
            get => (string)GetValue(RegexProperty);
            set => SetValue(RegexProperty, value);
        }

        public static readonly BindableProperty RegexOptionsProperty = BindableProperty.Create(
            propertyName: nameof(RegexOptions),
            returnType: typeof(RegexOptions),
            declaringType: typeof(CustomEntry),
            defaultValue: RegexOptions.None);

        public RegexOptions RegexOptions
        {
            get => (RegexOptions)GetValue(RegexOptionsProperty);
            set => SetValue(RegexOptionsProperty, value);
        }

        public static readonly BindableProperty ComparableStringProperty = BindableProperty.Create(
            propertyName: nameof(ComparableString),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string ComparableString
        {
            get => (string)GetValue(ComparableStringProperty);
            set => SetValue(ComparableStringProperty, value);
        }

        public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(
            propertyName: nameof(ErrorMessage),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string ErrorMessage
        {
            get => (string)GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(CustomEntry control)
        {
            base.OnAttachedTo(control);

            if (control != null)
            {
                _control = control;
                _control.ErrorText = ErrorMessage;
                _control.Entry.TextChanged += OnTextChanged;
            }
        }

        protected override void OnDetachingFrom(CustomEntry control)
        {
            base.OnDetachingFrom(control);

            if (_control != null)
            {
                _control.Entry.TextChanged -= OnTextChanged;
                _control = null;
            }
        }

        #endregion

        #region -- Private Helpers --

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var newValue = e.NewTextValue;

            if (!string.IsNullOrEmpty(newValue))
            {
                _control.IsValid = CheckValidity(newValue);
                _control.IsErrorVisible = !_control.IsValid;
            }
        }

        private bool CheckValidity(string value)
        {
            return !string.IsNullOrEmpty(ComparableString)
                ? value.Equals(ComparableString)
                : Validator.IsMatch(value, Regex, RegexOptions);
        }

        #endregion
    }
}
