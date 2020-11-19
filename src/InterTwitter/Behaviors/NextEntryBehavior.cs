using InterTwitter.Controls;
using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InterTwitter.Behaviors
{
    public class NextEntryBehavior : BehaviorBase<Entry>
    {
        #region -- Public Properties --

        public static readonly BindableProperty NextEntryProperty = BindableProperty.Create(
            propertyName: nameof(NextEntry), 
            returnType: typeof(Entry),
            declaringType: typeof(Entry));

        public Entry NextEntry
        {
            get => (Entry)GetValue(NextEntryProperty);
            set => SetValue(NextEntryProperty, value);
        }

        #endregion

        #region -- Overrides --

        protected override void OnAttachedTo(Entry entry)
        {
            entry.Completed += CustomEntryCompleted;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.Completed -= CustomEntryCompleted;
            base.OnDetachingFrom(entry);
        }

        #endregion

        #region -- Private Helpers --

        private void CustomEntryCompleted(object sender, EventArgs e)
        {
            if (NextEntry != null)
            {
                NextEntry.Focus();
            }
        }

        #endregion
    }
}
