using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    class CustomLabel : Label
    {
        public static BindableProperty ShouldHighlightTextProperty =
            BindableProperty.Create(nameof(ShouldHighlightText), typeof(bool), typeof(CustomLabel));
        public bool ShouldHighlightText
        {
            get => (bool)GetValue(ShouldHighlightTextProperty);
            set => SetValue(ShouldHighlightTextProperty, value);
        }

        public static BindableProperty SearchedTextProperty =
            BindableProperty.Create(nameof(ShouldHighlightText), typeof(string), typeof(CustomLabel));
        public string SearchedText
        {
            get => (string)GetValue(SearchedTextProperty);
            set => SetValue(SearchedTextProperty, value);
        }
    }
}
