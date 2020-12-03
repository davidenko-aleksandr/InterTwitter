using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using InterTwitter.Helpers;
using System.Runtime.CompilerServices;

namespace InterTwitter.Controls
{
    public class CustomLabel : Label
    {
        private static string _text;
        public CustomLabel()
        {

        }

        #region -- Public Properties --

        public static BindableProperty SearchedTextProperty =
            BindableProperty.Create(nameof(SearchedText), typeof(string), typeof(CustomLabel), defaultValue: null,
                propertyChanged: OnSearchedTextPropertyChanged);

        public string SearchedText
        {
            get => (string)GetValue(SearchedTextProperty);
            set => SetValue(SearchedTextProperty, value);
        }

        #endregion

        #region -- Private Helpers --

        private static void OnSearchedTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var label = bindable as CustomLabel;
            var newSearchQuery = (string)newValue;

            if (string.IsNullOrEmpty(newSearchQuery))
            {
                if (label != null)
                {
                    label.FormattedText = null;
                    label.Text = _text;
                }
            }
            else
            {
                label.Text = null;
                var matchingIndexes = KMPSearch.SearchString(_text.ToUpper(), newSearchQuery.ToUpper());
                label.FormattedText = CreateFormattedText(matchingIndexes, newSearchQuery);
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Text))
            {
                if (Text != null)
                {
                    _text = Text;
                }
            }
        }

        private static FormattedString CreateFormattedText(int[] matchingIndexes, string newSearchQuery)
        {
            FormattedString formattedText = new FormattedString();
            int j = 0;
            int a = 0;

            if (matchingIndexes.Length != 0)
            {
                for (int i = 0; i < _text.Length - 1; i++)
                {
                    if (i == matchingIndexes[j])
                    {
                        string usualString = string.Empty;

                        for (; a < matchingIndexes[j]; a++)
                        {
                            usualString += _text[a];
                        }

                        var usualSpan = new Span
                        {
                            Text = usualString
                        };

                        string highlightedString = string.Empty;

                        for (int b = matchingIndexes[j]; b < matchingIndexes[j] + newSearchQuery.Length; b++)
                        {
                            highlightedString += _text[b];
                        }

                        var highlightedSpan = new Span
                        {
                            Text = highlightedString,
                            BackgroundColor = Color.FromHex("#C7D6F7")
                        };

                        formattedText.Spans.Add(usualSpan);
                        formattedText.Spans.Add(highlightedSpan);

                        if (j + 1 < matchingIndexes.Length)
                        {
                            j++;
                        }
                        else
                        {
                            string lastString = string.Empty;

                            for (; a < _text.Length - 1; a++)
                            {
                                lastString += _text[a];
                            }

                            var lastSpan = new Span
                            {
                                Text = usualString
                            };

                            formattedText.Spans.Add(lastSpan);

                            break;
                        }
                    }
                }
            }

            return formattedText;
        }

        #endregion
    }
}
