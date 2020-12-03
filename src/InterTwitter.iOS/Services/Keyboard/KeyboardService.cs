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
        public float FrameHeight { get; set; }

        #endregion

        #region -- Private helpers --

        private void SubscribeEvents()
        {
            UIKeyboard.Notifications.ObserveWillShow(OnKeyboardDidShow);
            UIKeyboard.Notifications.ObserveWillHide(OnKeyboardDidHide);
        }

        private void OnKeyboardDidShow(object sender, UIKeyboardEventArgs e)
        {
            FrameHeight = (float)e.FrameEnd.Size.Height;

            KeyboardShown?.Invoke(this, EventArgs.Empty);
        }

        private void OnKeyboardDidHide(object sender, UIKeyboardEventArgs e)
        {
            KeyboardHidden?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}