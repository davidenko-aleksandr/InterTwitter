using InterTwitter.Enums;
using System.Diagnostics;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public class CustomImageButton : ImageButton
    {
        private static double DefaultButtonImageY { get; set; }
        public CustomImageButton()
        {
           
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
            //var button = bindable as CustomImageButton;
            //var newState = (MovingStates)newValue;

            //if (button is not null)
            //{
            //    switch (newState)
            //    {
            //        case MovingStates.MovingUp:
            //            {
            //                button.TranslateTo(button.X, button.Y + button.Height);
            //                break;
            //            }
            //        case MovingStates.MovingDown:
            //            {
            //                button.TranslateTo(button.X, button.Y - button.Height);
            //                break;
            //            }
            //        default:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else
            //{
            //    Debug.WriteLine("Tab is null");
            //}
        }

        #endregion
    }
}
