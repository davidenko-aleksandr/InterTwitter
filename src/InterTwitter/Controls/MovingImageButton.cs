using InterTwitter.Enums;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class MovingImageButton : ImageButton
    {
        public MovingImageButton()
        {
            Opacity = 0;
        }

        #region -- Public properties --

        public static BindableProperty MovingStateProperty =
           BindableProperty.Create(nameof(MovingState), typeof(MovingStates), typeof(MovingImageButton), propertyChanged: OnMovingStatePropertyChanged);
        public MovingStates MovingState
        {
            get => (MovingStates)GetValue(MovingStateProperty);
            set => SetValue(MovingStateProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void OnMovingStatePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var frame = bindable as MovingImageButton;
            var oldOffset = (MovingStates)oldvalue;
            var newOffset = (MovingStates)newValue;

            if (frame is not null && oldOffset != newOffset)
            {
                switch (newOffset)
                {
                    case MovingStates.MovingUp:
                        {
                            frame.TranslateTo(0, frame.Height * 2);
                            frame.FadeTo(0, 300);
                            break;
                        }

                    case MovingStates.MovingDown:
                        {
                            frame.TranslateTo(0, 0);
                            frame.FadeTo(1, 300);
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                //button is null or oldOffset == newOffset
            }
        }

        #endregion
    }
}
