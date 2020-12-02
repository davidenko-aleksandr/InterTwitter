using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.ViewModels.Helpers
{
    public interface IAppearingAware
    {
        void OnAppearing();

        void OnDisappearing();
    }
}
