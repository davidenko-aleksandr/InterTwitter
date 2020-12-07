using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterTwitter.Helpers
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public Grouping(K header, IEnumerable<T> items)
        {
            Header = header;
            foreach (T item in items)
            {
                Items.Add(item);
            }

            Amount = Count.ToString();
        }

        #region -- Public properties --

        public string Amount { get; set; }

        public K Header { get; private set; }

        #endregion
    }
}
