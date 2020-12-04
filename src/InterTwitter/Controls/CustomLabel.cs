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
        private string _text;
        public CustomLabel()
        {
        }

        #region -- Public Properties --

        public static BindableProperty SearchedTextProperty =
            BindableProperty.Create(nameof(SearchedText), typeof(string), typeof(CustomLabel), defaultValue: null, propertyChanged: OnSearchedTextPropertyChanged);

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

            if (newSearchQuery != null)
            {
                if (label._text.ToUpper().Contains(newSearchQuery.ToUpper()))
                {
                    label.Text = null;
                    var matchingIndexes = KMPSearch.SearchString(label._text.ToUpper(), newSearchQuery.ToUpper());
                    label.FormattedText = label.CreateFormattedText(matchingIndexes, newSearchQuery);
                }
                else
                {
                    if (label != null)
                    {
                        label.FormattedText = null;
                        label.Text = label._text;
                    }
                }
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

        private FormattedString CreateFormattedText(int[] matchingIndexes, string newSearchQuery)
        {
            FormattedString formattedText = new FormattedString();
            int j = 0;
            int a = 0;

            if (matchingIndexes.Length != 0)
            {
                for (int i = 0; i < _text.Length; i++)
                {
                    if (i == matchingIndexes[j])
                    {
                        string usualString = string.Empty;

                        for (; a < matchingIndexes[j]; a++)
                        {
                            usualString += _text[a];
                        }

                        if (!string.IsNullOrEmpty(usualString))
                        {
                            var usualSpan = new Span
                            {
                                Text = usualString
                            };

                            formattedText.Spans.Add(usualSpan);
                        }

                        string highlightedString = string.Empty;

                        for (; a < matchingIndexes[j] + newSearchQuery.Length; a++)
                        {
                            highlightedString += _text[a];
                        }

                        if (!string.IsNullOrEmpty(highlightedString))
                        {
                            var highlightedSpan = new Span
                            {
                                Text = highlightedString,
                                BackgroundColor = Color.FromHex("#C7D6F7")
                            };

                            formattedText.Spans.Add(highlightedSpan);
                        }

                        if (j < matchingIndexes.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            string lastString = string.Empty;

                            for (; a < _text.Length; a++)
                            {
                                lastString += _text[a];
                            }

                            if (!string.IsNullOrEmpty(lastString))
                            {
                                var lastSpan = new Span
                                {
                                    Text = lastString
                                };

                                formattedText.Spans.Add(lastSpan);
                            }

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
