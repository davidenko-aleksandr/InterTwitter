using System;

namespace InterTwitter.Services.Keyboard
{
    public interface IKeyboardService
    {
        event EventHandler KeyboardShown;
        event EventHandler KeyboardHidden;
        public float FrameHeight { get; set; }
    }
}
