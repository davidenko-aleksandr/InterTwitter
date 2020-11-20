using InterTwitter.Controls;
using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static InterTwitter.Validators.Validator;

namespace InterTwitter.Behaviors
{
    class ValidationBehavior : BehaviorBase<CustomEntry>
    {
        private CustomEntry _control;

        #region -- Public Properties --

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly(
            propertyName: nameof(IsValid),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry),
            defaultValue: false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidPropertyKey, value);
        }

        public static readonly BindableProperty RegexProperty = BindableProperty.Create(
            propertyName: nameof(Regex),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string Regex
        {
            get => (string)GetValue(RegexProperty);
            set => SetValue(RegexProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
            propertyName: nameof(IsPassword),
            returnType: typeof(bool),
            declaringType: typeof(CustomEntry));

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
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

        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            propertyName: nameof(Message),
            returnType: typeof(string),
            declaringType: typeof(CustomEntry));

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(CustomEntry control)
        {
            base.OnAttachedTo(control);

            _control = control;
            _control.ErrorText = Message;
            _control.Entry.TextChanged += OnTextChanged;
        }


        protected override void OnDetachingFrom(CustomEntry control)
        {
            base.OnDetachingFrom(control);

            _control.Entry.TextChanged -= OnTextChanged;
            _control.ErrorText = null;
            _control = null;
        }

        #endregion

        #region -- Private Helpers --

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var newValue = e.NewTextValue;

            if (!string.IsNullOrEmpty(newValue))
            {
                IsValid = CheckValidity(newValue);

                _control.IsErrorVisible = !IsValid;
            }

            #endregion
        }

        private bool CheckValidity(string value)
        {
            bool isMatch;

            //if (IsPassword)
            //{
            //    isMatch = IsMatch(RegexPasswordContainsLower, value)
            //        && IsMatch(RegexPasswordContainsUpper, value)
            //        && IsMatch(RegexPasswordContainsNumber, value);
            //}
            //else
            //{
                isMatch = !string.IsNullOrEmpty(ComparableString)
                        ? value.Equals(ComparableString)
                        : !string.IsNullOrEmpty(Regex) && IsMatch(value, Regex);
            //}

            return isMatch;
        }
    }
}
