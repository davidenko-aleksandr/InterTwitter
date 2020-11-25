using InterTwitter.Enums;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace InterTwitter.Controls
{
    public partial class HomePageNavigationBar : Grid
    {
        public HomePageNavigationBar()
        {
            InitializeComponent();
        }

        #region -- Public properties --

        public static BindableProperty MovingStateProperty =
            BindableProperty.Create(nameof(MovingState), typeof(MovingStates), typeof(AuthorizationNavigationBar), MovingStates.DefautState,
                propertyChanged: OnMovingStatePropertyChanged);
        public MovingStates MovingState
        {
            get => (MovingStates)GetValue(MovingStateProperty);
            set => SetValue(MovingStateProperty, value);
        }

        public static BindableProperty GoBackCommandProperty =
            BindableProperty.Create(nameof(GoBackCommand), typeof(ICommand), null, typeof(AuthorizationNavigationBar));
        public ICommand GoBackCommand
        {
            get => (ICommand)GetValue(GoBackCommandProperty);
            set => SetValue(GoBackCommandProperty, value);
        }

        public static BindableProperty EditCommandProperty =
            BindableProperty.Create(nameof(EditCommand), typeof(ICommand), null, typeof(AuthorizationNavigationBar));
        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void OnMovingStatePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {

            var tab = bindable as HomePageNavigationBar;
            var newState = (MovingStates)newValue;

            if (tab is not null)
            {
                switch (newState)
                {
                    case MovingStates.MovingUp:
                        {
                            tab.TranslateTo(0, -tab.Width);
                            break;
                        }
                    case MovingStates.MovingDown:
                        {
                            tab.TranslateTo(0, tab.Width);
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
                Debug.WriteLine("Tab is null");
            }
        }

        #endregion
    }
}