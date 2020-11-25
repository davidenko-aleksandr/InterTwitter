using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Services.Keyboard
{
    public interface IKeyboardService
    {
        event EventHandler KeyboardShown;
        event EventHandler KeyboardHidden;
    }
}
