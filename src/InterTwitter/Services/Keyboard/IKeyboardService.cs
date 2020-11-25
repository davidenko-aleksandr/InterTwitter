using System;
using System.Collections.Generic;
using System.Text;

namespace InterTwitter.Services.Keyboard
{
    public interface IKeyboardService
    {
        event EventHandler KeyboardIsShown;
        event EventHandler KeyboardIsHidden;
    }
}
