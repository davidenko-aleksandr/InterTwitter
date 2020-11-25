using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using InterTwitter.Services.Keyboard;

namespace InterTwitter.iOS.Services.Keyboard
{
    public class KeyboardService : IKeyboardService
    {
        public KeyboardService()
        {
            SubscribeEvents();
        }

        #region -- IKeyboardService implementation --

        public event EventHandler KeyboardShown;
        public event EventHandler KeyboardHidden;

        #endregion

        #region -- Private helpers --

        private void SubscribeEvents()
        {
            UIKeyboard.Notifications.ObserveDidShow(OnKeyboardDidShow);
            UIKeyboard.Notifications.ObserveDidHide(OnKeyboardDidHide);
        }

        private void OnKeyboardDidShow(object sender, EventArgs e)
        {
            KeyboardShown?.Invoke(this, EventArgs.Empty);
        }

        private void OnKeyboardDidHide(object sender, EventArgs e)
        {
            KeyboardHidden?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}