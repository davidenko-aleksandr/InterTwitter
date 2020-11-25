using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterTwitter.Services.Keyboard;
using Android.Views.InputMethods;

namespace InterTwitter.Droid.Services.Keyboard
{
    public class KeyboardService : IKeyboardService
    {
        private InputMethodManager _inputMethodManager;
        private bool _wasShown = false;

        public KeyboardService()
        {
            GetInputMethodManager();
            SubscribeEvents();
        }

        #region -- IKeyboardService implementation --

        public event EventHandler KeyboardShown;
        public event EventHandler KeyboardHidden;

        #endregion

        #region -- Private helpers --

        private void OnGlobalLayout(object sender, EventArgs args)
        {
            GetInputMethodManager();
            if (!_wasShown && IsCurrentlyShown())
            {
                KeyboardShown?.Invoke(this, EventArgs.Empty);
                _wasShown = true;
            }
            else if (_wasShown && !IsCurrentlyShown())
            {
                KeyboardHidden?.Invoke(this, EventArgs.Empty);
                _wasShown = false;
            }
        }

        private bool IsCurrentlyShown()
        {
            return _inputMethodManager.IsAcceptingText;
        }

        private void GetInputMethodManager()
        {
            if (_inputMethodManager == null || _inputMethodManager.Handle == IntPtr.Zero)
            {
                _inputMethodManager = (InputMethodManager)Xamarin.Forms.Forms.Context.GetSystemService(Context.InputMethodService);
            }
        }

        private void SubscribeEvents()
        {
            ((Activity)Xamarin.Forms.Forms.Context).Window.DecorView.ViewTreeObserver.GlobalLayout += OnGlobalLayout;
        }

        #endregion
    }
}
