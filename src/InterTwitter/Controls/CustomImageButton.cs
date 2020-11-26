using InterTwitter.Enums;
using System.Diagnostics;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomImageButton : ImageButton
    {
        public CustomImageButton()
        {
            MovingState = MovingStates.MovingDown;
        }
        #region -- Public properties --

        public static BindableProperty MovingStateProperty =
           BindableProperty.Create(nameof(MovingState), typeof(MovingStates), typeof(AuthorizationNavigationBar), propertyChanged: OnMovingStatePropertyChanged);
        public MovingStates MovingState
        {
            get => (MovingStates)GetValue(MovingStateProperty);
            set => SetValue(MovingStateProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void OnMovingStatePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var button = bindable as CustomImageButton;
            var oldOffset = (MovingStates)oldvalue;
            var newOffset = (MovingStates)newValue;

            if (button is not null && oldOffset != newOffset)
            {
                switch (newOffset)
                {
                    case MovingStates.MovingUp:
                        {
                            button.TranslateTo(0, button.Height * 2);
                            break;
                        }
                    case MovingStates.MovingDown:
                        {
                            button.TranslateTo(0, 0);
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
                //button is null of oldOffset == newOffset 
            }
        }

        #endregion
    }
}
