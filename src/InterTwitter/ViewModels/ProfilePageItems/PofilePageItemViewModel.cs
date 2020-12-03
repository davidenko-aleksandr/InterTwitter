using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.ViewModels.ProfilePageItems
{
   public class PofilePageItemViewModel : BindableBase
    {
        public PofilePageItemViewModel(string title)
        {
            Title = title;
        }

        #region -- Public properties --

        public string Title { get; set; }

        #endregion
    }
}
